using System;
using System.Windows.Data;
using System.Windows.Markup;

namespace LockpickersGuide.ViewLogic
{
    public class SubtractConverter : MarkupExtension, IValueConverter
    {
        public double Value { get; set; }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double val = System.Convert.ToDouble(value);
            // Change here if you want other operations
            return val - Value;
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
