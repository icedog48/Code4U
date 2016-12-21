using Code4U.Helpers;
using Code4U.Models;
using DatabaseSchemaReader;
using DatabaseSchemaReader.DataSchema;
using MediatR;
using RazorEngine;
using RazorEngine.Compilation.ReferenceResolver;
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
    public class RunTemplate : IRequest<string>
    {
        public Project Model { get; set; }
    }

    public class RunTemplateHandler : IRequestHandler<RunTemplate, string>
    {
        public string Handle(RunTemplate message)
        {
            string[] templateFolders = null;

            try
            {
                templateFolders = FileSystemHelper.GetDirectories(message.Model.TemplateFolder);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Template folder cannot be empty.", ex);
            }

            ConfigureRazorEngine(templateFolders);

            var templateFileName = Path.Combine(message.Model.TemplateFolder, "Index.cshtml");

            return Engine.Razor.RunCompile(templateFileName, typeof(Project), message.Model);
        }

        private void ConfigureRazorEngine(string[] templateFolders)
        {
            var config = new TemplateServiceConfiguration()
            {
                Debug = true,
                Language = Language.CSharp,
                TemplateManager = new ResolvePathTemplateManager(templateFolders),
                EncodedStringFactory = new RawStringFactory(),
                ReferenceResolver = new Code4UReferenceResolver(templateFolders.First())
            };

            var service = RazorEngineService.Create(config);

            Engine.Razor = service;
        }
    }
}
