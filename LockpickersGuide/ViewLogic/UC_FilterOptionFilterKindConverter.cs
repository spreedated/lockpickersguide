#pragma warning disable S101

using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace LockpickersGuide.ViewLogic
{
    public class UC_FilterOptionFilterKindConverter : MarkupExtension, IValueConverter
    {
        public FilterOption.Kinds ElementKind { get; set; }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            FilterOption.Kinds val = (FilterOption.Kinds)value;
            return ElementKind == val ? Visibility.Visible : Visibility.Collapsed;
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
