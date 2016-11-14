﻿using Code4U.Helpers;
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
using System.Text;
using System.Threading.Tasks;

namespace Code4U.Commands
{
    public class GetModelByDataBaseSchema : IRequest<Project>
    {
        public string ProjectName { get; set; }

        public string TemplateFolder { get; set; }

        public string GeneratedCodeFolder { get; set; }

        public string Server { get; set; }

        public string Database { get; set; }

        public string User { get; set; }

        public string Password { get; set; }
    }

    public class GetModelByDataBaseSchemaHandler : IRequestHandler<GetModelByDataBaseSchema, Project>
    {
        public Project Handle(GetModelByDataBaseSchema message)
        {
            //ConfigureRazorEngine(FileSystemHelper.GetDirectories(message.TemplateFolder));

            //var project = ReadDatabaseSchema(message);

            //var templateFileName = Path.Combine(message.TemplateFolder, "Index.cshtml");

            //return Engine.Razor.RunCompile(templateFileName, typeof(Project), project);

            var connectionString = $"Server={message.Server};Database={message.Database};User Id={message.User};Password={message.Password};";

            var dbReader = new DatabaseReader(connectionString, "System.Data.SqlClient");

            var schema = dbReader.ReadAll();

            var project = new Project()
            {
                Name = message.ProjectName,
                Entities = GetEntities(schema),
                GeneratedCodeFolder = message.GeneratedCodeFolder,
                TemplateFolder = message.TemplateFolder
            };

            return project;
        }

        private IEnumerable<Entity> GetEntities(DatabaseSchema schema)
        {
            var entities = new List<Entity>();

            foreach (var table in schema.Tables)
            {
                var entity = new Entity()
                {
                    Name = table.Name,
                    Properties = GetProperties(table)
                };
                
                entities.Add(entity);
            }

            return entities;
        }

        private IEnumerable<Property> GetProperties(DatabaseTable table)
        {
            var properties = new List<Property>();

            foreach (var column in table.Columns)
            {
                var property = new Property();

                property.Name = column.Name;
                if (column.IsForeignKey) property.Name = column.ForeignKeyTableName;

                property.Type = column.DataType.NetDataTypeCSharpName;
                if (column.IsForeignKey) property.Type = column.ForeignKeyTableName;

                property.Size = column.Length;

                property.Identity = column.IsAutoNumber;

                property.IsPrimaryKey = column.IsPrimaryKey;

                property.IsForeignKey = column.IsForeignKey;

                property.IsNullable = column.Nullable;

                if (column.IsUniqueKey)
                {
                    property.UniqueKeyName = $"UQ_{column.TableName}_{column.Name}";
                }

                property.DefaultValue = column.DefaultValue;

                properties.Add(property);
            }

            return properties;
        }

        private void ConfigureRazorEngine(string[] templateFolders)
        {
            var config = new TemplateServiceConfiguration()
            {
                Debug = true,
                Language = Language.CSharp,
                TemplateManager = new ResolvePathTemplateManager(templateFolders),
                EncodedStringFactory = new RawStringFactory()
            };

            var service = RazorEngineService.Create(config);

            Engine.Razor = service;
        }
    }
}