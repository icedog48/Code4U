using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code4U.WinForm.Forms.ViewModels
{
    public class ProjectViewModel
    {
        [Category(Program.GENERAL_CATEGORY)]
        [DisplayName("Name")]
        public string Name { get; set; }

        [Category(Program.BEHAVIOR_CATEGORY)]
        [DisplayName("Template Folder")]
        public string TemplateFolder { get; set; }

        [Category(Program.BEHAVIOR_CATEGORY)]
        [DisplayName("Generated Code Folder")]
        public string GeneratedCodeFolder { get; set; }

        [Browsable(false)]
        public IEnumerable<EntityViewModel> Entities { get; set; }
    }
}
