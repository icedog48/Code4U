using Code4U.Helpers;
using DatabaseSchemaReader;
using DatabaseSchemaReader.DataSchema;
using MediatR;
using RazorEngine;
using RazorEngine.Configuration;
using RazorEngine.Templating;
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

            var schema = ReadDatabaseSchema(message);

            var viewBag = new DynamicViewBag();
            viewBag.AddValue("TemplateFolder", message.TemplateFolder);
            viewBag.AddValue("GeneratedCodeFolder", message.GeneratedCodeFolder);

            var templateFileName = Path.Combine(message.TemplateFolder, "Index.cshtml");

            return Engine.Razor.RunCompile(templateFileName, typeof(DatabaseSchema), schema, viewBag);
        }

        private DatabaseSchema ReadDatabaseSchema(RunDatabaseSchemaTemplate message)
        {
            var connectionString = $"Server={message.Server};Database={message.Database};User Id={message.User};Password={message.Password};";

            var dbReader = new DatabaseReader(connectionString, "System.Data.SqlClient");
            
            return dbReader.ReadAll();
        }

        private void ConfigureRazorEngine(string[] templateFolders)
        {
            var config = new TemplateServiceConfiguration()
            {
                Debug = true,
                Language = Language.CSharp,
                TemplateManager = new ResolvePathTemplateManager(templateFolders)
            };

            var service = RazorEngineService.Create(config);

            Engine.Razor = service;
        }
    }
}
