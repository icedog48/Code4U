using Code4U.Helpers;
using Code4U.Models;
using DatabaseSchemaReader;
using DatabaseSchemaReader.DataSchema;
using MediatR;
using RazorEngine;
using RazorEngine.Configuration;
using RazorEngine.Templating;
using RazorEngine.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections;

namespace Code4U.Commands
{
    public static class TypeExtensions
    {
        public static string ToGenericTypeString(this Type type)
        {
            if (!type.IsGenericType) return type.GetSystemType();

            var genericTypeName = type.GetGenericTypeDefinition().Name;

            genericTypeName = genericTypeName.Substring(0, genericTypeName.IndexOf('`'));

            var genericArgs = string.Join(",", type.GetGenericArguments().Select(ta => ToGenericTypeString(ta)).ToArray());

            return genericTypeName + "<" + genericArgs + ">";
        }

        public static string GetSystemType(this Type type)
        {
            if (type.Name == "Int32") return "int";

            if (type.Name == "String") return "string";

            if (type.Name == "Boolean") return "bool";

            return type.Name;
        }
    }

    public class GetModelFromAssembly : IRequest<Project>
    {
        public string ProjectName { get; set; }

        public string AssemblyFilename { get; set; }

        public string Namespace { get; set; }
    }

    public class GetModelFromAssemblyHandler : IRequestHandler<GetModelFromAssembly, Project>
    {
        public Project Handle(GetModelFromAssembly message)
        {
            var assembly = Assembly.LoadFrom(message.AssemblyFilename);

            var assemblyTypes = assembly.GetExportedTypes().Where(x => x.Namespace == message.Namespace).Select(x => x.GetTypeInfo()).ToList();

            var project = new Project()
            {
                Name = message.ProjectName,
                Entities = GetEntities(assemblyTypes)
            };

            return project;
        }

        private IEnumerable<Entity> GetEntities(IEnumerable<TypeInfo> assemblyTypes)
        {
            var entities = new List<Entity>();

            foreach (var type in assemblyTypes)
            {
                var entity = new Entity()
                {
                    Name = type.Name,
                    Properties = GetProperties(type)
                };
                
                entities.Add(entity);
            }

            return entities;
        }

        private IEnumerable<Property> GetProperties(TypeInfo type)
        {
            var properties = new List<Property>();

            var typeProperties = type.DeclaredProperties;

            foreach (var typeProperty in typeProperties)
            {
                var property = new Property();

                property.Name = typeProperty.Name;

                var propertyType = typeProperty.PropertyType;
                
                if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    propertyType = Nullable.GetUnderlyingType(propertyType);

                    property.Type = propertyType.GetSystemType();
                    property.Flags.Add("Nullable");
                }
                else if (propertyType.IsGenericType)
                {
                    if (typeof(IEnumerable).IsAssignableFrom(propertyType))
                    {
                        property.Flags.Add("Collection");
                        property.Flags.Add("Nullable");
                    }
                              
                    property.Type = propertyType.ToGenericTypeString();
                }
                else
                {
                    property.Type = propertyType.GetSystemType();
                }

                if (!propertyType.Namespace.Contains("System"))
                {
                    property.Flags.Add("ForeignKey");
                }

                properties.Add(property);
            }

            return properties;
        }
    }
}
