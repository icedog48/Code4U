using DatabaseSchemaReader.DataSchema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code4U.Models
{
    public class Entity
    {
        public string Name { get; set; }

        public IEnumerable<Property> Properties { get; set; }

        public bool IsLastProperty(Property property)
        {
            var lastProperty = this.Properties.Last();

            return property == lastProperty;
        }
    }
}
