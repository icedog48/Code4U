using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code4U.Models
{
    public class Project
    {
        public string Name { get; set; }

        public string TemplateFolder { get; set; }

        public string GeneratedCodeFolder { get; set; }

        public IEnumerable<Entity> Entities { get; set; }
    }
}
