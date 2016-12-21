using DatabaseSchemaReader.DataSchema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code4U.Models
{
    public class Project
    {
        private IList<Entity> entities = new List<Entity>();

        public Project() { }

        public string Name { get; set; }

        public string TemplateFolder { get; set; }

        public string GeneratedCodeFolder { get; set; }

        public IEnumerable<Entity> Entities 
        {
            get { return entities; }
            set
            {
                var entityList = value.ToList();

                foreach (var entity in entityList)
                {
                    entity.Project = this;
                }

                entities = entityList;
            }
        }
    }
}
