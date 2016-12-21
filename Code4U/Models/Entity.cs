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
        private IList<Property> properties = new List<Property>();

        public Entity() { }

        public string Name { get; set; }

        public IEnumerable<Property> Properties 
        {
            get { return properties; }
            set
            {
                var propertyList = value.ToList();

                foreach (var property in propertyList)
                {
                    property.Entity = this;
                }

                properties = propertyList;
            }
        }

        public bool IsLastProperty(Property property)
        {
            var lastProperty = this.Properties.Last();

            return property == lastProperty;
        }

        public Project Project { get; set; }
    }
}
