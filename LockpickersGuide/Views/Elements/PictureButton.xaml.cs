using MaterialDesignThemes.Wpf;
using System;
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

namespace LockpickersGuide.Views.Elements
{
    /// <summary>
    /// Interaction logic for PictureButton.xaml
    /// </summary>
    public partial class PictureButton : UserControl
    {
        public PackIconKind IconKind
        {
            get => (PackIconKind)GetValue(IconKindProperty);
            set => SetValue(IconKindProperty, value);
        }

        public static readonly DependencyProperty IconKindProperty =
        DependencyProperty.Register(
            name: "IconKind",
            propertyType: typeof(PackIconKind),
            ownerType: typeof(PictureButton),
            typeMetadata: new UIPropertyMetadata(PackIconKind.None));

        public string ButtonText
        {
            get => (string)GetValue(ButtonTextProperty);
            set => SetValue(ButtonTextProperty, value);
        }

        public static readonly DependencyProperty ButtonTextProperty =
        DependencyProperty.Register(
            name: "ButtonText",
            propertyType: typeof(string),
            ownerType: typeof(PictureButton),
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
            ownerType: typeof(PictureButton),
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
            ownerType: typeof(PictureButton),
            typeMetadata: new UIPropertyMetadata(null));

        public PictureButton()
        {
            InitializeComponent();
        }
    }
}
