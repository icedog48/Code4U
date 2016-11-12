using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code4U.WinForm.Forms.ViewModels.Converters
{
    public class StringListConverter : TypeConverter
    {
        public const string STRING_LIST_EDITOR = @"System.Windows.Forms.Design.StringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";
        
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            var values = value as List<String>;

            if (destinationType == typeof(string))
            {
                if (values == null) return string.Empty;

                return String.Join(",", values.ToArray());
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
