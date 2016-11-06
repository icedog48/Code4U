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
        public string Name { get; set; }

        public string Type { get; set; }

        public int? Size { get; set; }

        public bool Identity { get; set; }

        public bool IsPrimaryKey { get; set; }

        public bool IsForeignKey { get; set; }

        public bool IsNullable { get; set; }

        public string UniqueKeyName { get; set; }

        public string DefaultValue { get; set; }
    }
}