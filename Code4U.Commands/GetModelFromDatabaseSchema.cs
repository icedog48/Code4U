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
using System.Text;
using System.Threading.Tasks;

namespace Code4U.Commands
{
    public class GetModelFromDatabaseSchema : IRequest<Project>
    {
        public string ProjectName { get; set; }

        public string Server { get; set; }

        public string Database { get; set; }

        public string User { get; set; }

        public string Password { get; set; }
    }

    public class GetModelFromDatabaseSchemaHandler : IRequestHandler<GetModelFromDatabaseSchema, Project>
    {
        public Project Handle(GetModelFromDatabaseSchema message)
        {
            var connectionString = $"Server={message.Server};Database={message.Database};User Id={message.User};Password={message.Password};";

            var dbReader = new DatabaseReader(connectionString, "System.Data.SqlClient");

            var schema = dbReader.ReadAll();

            var project = new Project()
            {
                Name = message.ProjectName,
                Entities = GetEntities(schema)
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

                if (column.IsAutoNumber) property.Flags.Add("Identity");

                if (column.IsPrimaryKey) property.Flags.Add("PrimaryKey");

                if (column.IsForeignKey) property.Flags.Add("ForeignKey");

                if (column.Nullable) property.Flags.Add("Nullable");

                if (column.IsUniqueKey) property.Flags.Add("UniqueKey");

                property.DefaultValue = column.DefaultValue;

                properties.Add(property);
            }

            return properties;
        }
    }
}
