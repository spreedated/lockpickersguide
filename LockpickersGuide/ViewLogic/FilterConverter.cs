using System;
using System.Windows.Data;
using System.Windows.Markup;
using MaterialDesignThemes.Wpf;

namespace LockpickersGuide.ViewLogic
{
    public class FilterConverter : MarkupExtension, IValueConverter
    {
        public bool IsText { get; set; }
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool val = System.Convert.ToBoolean(value);

            if (this.IsText)
            {
                return (val ? "Filter applied" : "Apply filter");
            }


            return val ? PackIconKind.Filter : PackIconKind.FilterOff;
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
