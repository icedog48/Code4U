using DatabaseSchemaReader.DataSchema;
using RazorEngine;
using RazorEngine.Templating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code4U.WinForm.Helpers
{
    public static class RazorEngineHelper
    {
        public static string GetGeneratedCode(string filename, DatabaseTable table)
        {
            return Engine.Razor.RunCompile(filename, typeof(DatabaseTable), table);
        }
    }
}
