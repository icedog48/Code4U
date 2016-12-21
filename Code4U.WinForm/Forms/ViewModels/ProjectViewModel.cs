using Code4U.WinForm.Forms.ViewModels.TypeEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code4U.WinForm.Forms.ViewModels
{
    public class GetFromAssemblyViewModel 
    {
        public string AssemblyFilename { get; set; }

        public string Namespace { get; set; }
    }

    public class GetFromDatabaseViewModel 
    {
        public string Server { get; set; }

        public string Database { get; set; }

        public string User { get; set; }

        public string Password { get; set; }
    }

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

        public GetFromAssemblyViewModel FromAssembly { get; set; }

        public GetFromDatabaseViewModel FromDatabase { get; set; }

        [Browsable(false)]
        public IEnumerable<EntityViewModel> Entities { get; set; }
    }
}
