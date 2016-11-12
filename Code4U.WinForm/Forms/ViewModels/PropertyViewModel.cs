using Code4U.WinForm;
using DatabaseSchemaReader.DataSchema;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Code4U.WinForm.Forms.ViewModels.Converters;

namespace Code4U.Models
{
    public class PropertyViewModel
    {
        [Category(Program.GENERAL_CATEGORY)]
        public string Label { get; set; }

        [Category(Program.GENERAL_CATEGORY)]
        public string Name { get; set; }

        [Category(Program.GENERAL_CATEGORY)]
        public string Type { get; set; }

        [Category(Program.BEHAVIOR_CATEGORY)]
        public int? Size { get; set; }

        [Category(Program.BEHAVIOR_CATEGORY)]
        public string DefaultValue { get; set; }

        [Category(Program.BEHAVIOR_CATEGORY)]
        [Editor(StringListConverter.STRING_LIST_EDITOR, typeof(System.Drawing.Design.UITypeEditor))]
        [TypeConverter(typeof(StringListConverter))]
        public IList<string> Flags { get; set; }
    }
}