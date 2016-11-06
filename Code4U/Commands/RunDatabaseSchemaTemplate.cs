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
    public class RunDatabaseSchemaTemplate : IRequest<string>
    {
        public string TemplateFolder { get; set; }

        public string GeneratedCodeFolder { get; set; }

        public string Server { get; set; }

        public string Database { get; set; }

        public string User { get; set; }

        public string Password { get; set; }
    }

    public class RunDatabaseSchemaTemplateHandler : IRequestHandler<RunDatabaseSchemaTemplate, string>
    {
        public string Handle(RunDatabaseSchemaTemplate message)
        {
            ConfigureRazorEngine(FileSystemHelper.GetDirectories(message.TemplateFolder));

            var project = ReadDatabaseSchema(message);

            var viewBag = new DynamicViewBag();
            viewBag.AddValue("TemplateFolder", message.TemplateFolder);
            viewBag.AddValue("GeneratedCodeFolder", message.GeneratedCodeFolder);

            var templateFileName = Path.Combine(message.TemplateFolder, "Index.cshtml");

            return Engine.Razor.RunCompile(templateFileName, typeof(Project), project, viewBag);
        }

        private Project ReadDatabaseSchema(RunDatabaseSchemaTemplate message)
        {
            var connectionString = $"Server={message.Server};Database={message.Database};User Id={message.User};Password={message.Password};";

            var dbReader = new DatabaseReader(connectionString, "System.Data.SqlClient");

            var schema = dbReader.ReadAll();

            var project = new Project()
            {
                Entities = GetEntities(schema),
                GeneratedCodeFolder = message.GeneratedCodeFolder,
                TemplateFolder = message.TemplateFolder,
                DatabaseSchema = schema
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
                    Properties = GetProperties(table),
                    DbTable = table
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
                properties.Add(new Property(column));
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
