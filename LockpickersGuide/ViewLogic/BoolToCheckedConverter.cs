using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows;
using LockpickersGuide.Logic;

namespace LockpickersGuide.ViewLogic
{
    public class BoolToCheckedConverter : MarkupExtension, IValueConverter
    {
        public bool IsCheckbox { get; set; }
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (IsCheckbox && (value is bool boolean))
            {
                bool val = value.IsSet() && boolean;
                return val;
            }
            else
            {
                return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
