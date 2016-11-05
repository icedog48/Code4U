using DatabaseSchemaReader.DataSchema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorEngine.Templating
{
    public abstract class Code4UTemplateBase<T> : TemplateBase<T>
    {
        public virtual string IncludeInLine(string name, object model, Type modelType)
        {
            return this.Include(name, model, modelType).ToString().Replace(Environment.NewLine, string.Empty);
        }
    }
}
