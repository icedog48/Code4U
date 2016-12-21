using DatabaseSchemaReader.DataSchema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code4U.Models
{
    public class Property
    {
        public Property()
        {
            this.Flags = new List<string>();
        }

        public string Label { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public int? Size { get; set; }

        public string DefaultValue { get; set; }

        public IList<string> Flags { get; set; }

        public bool HasFlag(string flag)
        {
            return this.Flags.Any(x => x.ToLower().Trim() == flag.ToLower().Trim());
        }

        public Entity Entity { get; set; }
    }
}