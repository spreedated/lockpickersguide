using LockpickersGuide.Logic;
using LockpickersGuide.ViewLogic;
using LockpickersGuide.Views.Elements;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LockpickersGuide.Views.UC
{
    /// <summary>
    /// Interaction logic for UC_FilterOption.xaml
    /// </summary>
    public partial class UC_FilterOption : UserControl
    {
        public IEnumerable ComboboxItemSource
        {
            get => (IEnumerable)GetValue(ComboboxItemSourceProperty);
            set => SetValue(ComboboxItemSourceProperty, value);
        }

        public static readonly DependencyProperty ComboboxItemSourceProperty =
        DependencyProperty.Register(
            name: "ComboboxItemSource",
            propertyType: typeof(IEnumerable),
            ownerType: typeof(UC_FilterOption),
            typeMetadata: new UIPropertyMetadata(null));

        public ICommand CheckBoxSet
        {
            get
            {
                return new RelayCommand((sender) =>
                {
                    if (!this.Result.IsSet())
                    {
                        this.Result = true;
                        return;
                    }
                    if (this.Result is bool)
                    {
                        if ((bool)this.Result)
                        {
                            this.Result = false;
                        }
                        else
                        {
                            this.Result = true;
                        }
                    }
                });
            }
        }

        public object Result
        {
            get => GetValue(ResultProperty);
            set => SetValue(ResultProperty, value);
        }

        public static readonly DependencyProperty ResultProperty =
        DependencyProperty.Register(
            name: "Result",
            propertyType: typeof(object),
            ownerType: typeof(UC_FilterOption),
            typeMetadata: new UIPropertyMetadata(null));

        public FilterOption.Kinds FilterKind
        {
            get => (FilterOption.Kinds)GetValue(FilterKindProperty);
            set => SetValue(FilterKindProperty, value);
        }

        public static readonly DependencyProperty FilterKindProperty =
        DependencyProperty.Register(
            name: "FilterKind",
            propertyType: typeof(FilterOption.Kinds),
            ownerType: typeof(UC_FilterOption),
            typeMetadata: new UIPropertyMetadata(FilterOption.Kinds.Text));

        public string Labeltext
        {
            get => (string)GetValue(LabeltextProperty);
            set => SetValue(LabeltextProperty, value);
        }

        public static readonly DependencyProperty LabeltextProperty =
        DependencyProperty.Register(
            name: "Labeltext",
            propertyType: typeof(string),
            ownerType: typeof(UC_FilterOption),
            typeMetadata: new UIPropertyMetadata("N/A"));

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public static readonly DependencyProperty CommandProperty =
        DependencyProperty.Register(
            name: "Command",
            propertyType: typeof(ICommand),
            ownerType: typeof(UC_FilterOption),
            typeMetadata: new UIPropertyMetadata(null));

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        public static readonly DependencyProperty CommandParameterProperty =
        DependencyProperty.Register(
            name: "CommandParameter",
            propertyType: typeof(object),
            ownerType: typeof(UC_FilterOption),
            typeMetadata: new UIPropertyMetadata(null));
        public UC_FilterOption()
        {
            InitializeComponent();
        }
    }
}
