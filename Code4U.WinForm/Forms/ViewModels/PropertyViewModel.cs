using Code4U.WinForm;
using DatabaseSchemaReader.DataSchema;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code4U.Models
{
    public class PropertyViewModel
    {
        [Category(Program.GENERAL_CATEGORY)]
        public string Name { get; set; }

        [Category(Program.BEHAVIOR_CATEGORY)]
        public string Type { get; set; }

        [Category(Program.BEHAVIOR_CATEGORY)]
        public int? Size { get; set; }

        [Category(Program.BEHAVIOR_CATEGORY)]
        public bool Identity { get; set; }

        [Category(Program.BEHAVIOR_CATEGORY)]
        public bool IsPrimaryKey { get; set; }

        [Category(Program.BEHAVIOR_CATEGORY)]
        public bool IsForeignKey { get; set; }

        [Category(Program.BEHAVIOR_CATEGORY)]
        public bool IsNullable { get; set; }

        [Category(Program.BEHAVIOR_CATEGORY)]
        public string UniqueKeyName { get; set; }

        [Category(Program.BEHAVIOR_CATEGORY)]
        public string DefaultValue { get; set; }
    }
}