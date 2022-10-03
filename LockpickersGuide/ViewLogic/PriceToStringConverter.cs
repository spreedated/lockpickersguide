using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace LockpickersGuide.ViewLogic
{
    public class PriceToStringConverter : MarkupExtension, IValueConverter
    {
        public string Currency { get; set; }
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            decimal vv = (decimal)value;

            if (vv == 0.0m)
            {
                return " --- ";
            }

            string val = ((decimal)value).ToString("0.00");

            return  $"{val} {this.Currency}";
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
