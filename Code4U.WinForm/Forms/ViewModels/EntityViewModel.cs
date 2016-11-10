using Code4U.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code4U.WinForm.Forms.ViewModels
{
    public class EntityViewModel
    {
        [Category(Program.GENERAL_CATEGORY)]
        [DisplayName("Name")]
        public string Name { get; set; }

        [Browsable(false)]
        public IEnumerable<PropertyViewModel> Properties { get; set; }
    }
}
