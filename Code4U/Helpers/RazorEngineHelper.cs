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

namespace Code4U.Helpers
{
    public static class RazorEngineHelper
    {
        public static void Run(string templateFilename) 
        {
            var config = new TemplateServiceConfiguration()
            {
                Debug = true,
                Language = Language.CSharp,
                TemplateManager = new ResolvePathTemplateManager(templateFolders),
                EncodedStringFactory = new RawStringFactory(),
                ReferenceResolver = new RazorEngineReferenceResolver(templateFolders.First())
            };

            var service = RazorEngineService.Create(config);

            Engine.Razor = service;
        }
    }
}
