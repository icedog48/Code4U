using Code4U.WinForm.Forms.ViewModels.TypeEditors;
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
        [Editor(typeof(FileSelectorTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string TemplateFolder { get; set; }

        [Category(Program.BEHAVIOR_CATEGORY)]
        [DisplayName("Generated Code Folder")]
        [Editor(typeof(FileSelectorTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string GeneratedCodeFolder { get; set; }
        
        [Browsable(false)]
        public IEnumerable<EntityViewModel> Entities { get; set; }
    }
}
