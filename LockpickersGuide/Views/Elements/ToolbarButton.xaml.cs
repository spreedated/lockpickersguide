using LockpickersGuide.ViewLogic;
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
    /// Interaction logic for ToolbarButton.xaml
    /// </summary>
    public partial class ToolbarButton : UserControl
    {
        public int InnerHeight
        {
            get => (int)GetValue(InnerHeightProperty);
            set => SetValue(InnerHeightProperty, value);
        }

        public static readonly DependencyProperty InnerHeightProperty =
        DependencyProperty.Register(
            name: "InnerHeight",
            propertyType: typeof(int),
            ownerType: typeof(ToolbarButton),
            typeMetadata: new UIPropertyMetadata(20));

        public int InnerWidth
        {
            get => (int)GetValue(InnerWidthProperty);
            set => SetValue(InnerWidthProperty, value);
        }

        public static readonly DependencyProperty InnerWidthProperty =
        DependencyProperty.Register(
            name: "InnerWidth",
            propertyType: typeof(int),
            ownerType: typeof(ToolbarButton),
            typeMetadata: new UIPropertyMetadata(20));

        public Thickness InnerMargin
        {
            get => (Thickness)GetValue(InnerMarginProperty);
            set => SetValue(InnerMarginProperty, value);
        }

        public static readonly DependencyProperty InnerMarginProperty =
        DependencyProperty.Register(
            name: "InnerMargin",
            propertyType: typeof(Thickness),
            ownerType: typeof(ToolbarButton),
            typeMetadata: new UIPropertyMetadata(new Thickness(0,0,0,0)));

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        public static readonly DependencyProperty CommandParameterProperty =
        DependencyProperty.Register(
            name: "CommandParameter",
            propertyType: typeof(object),
            ownerType: typeof(ToolbarButton),
            typeMetadata: new UIPropertyMetadata(null));

        public ICommand SomeCommand
        {
            get => (ICommand)GetValue(SomeCommandProperty);
            set => SetValue(SomeCommandProperty, value);
        }

        public static readonly DependencyProperty SomeCommandProperty =
        DependencyProperty.Register(
            name: "SomeCommand",
            propertyType: typeof(ICommand),
            ownerType: typeof(ToolbarButton),
            typeMetadata: new UIPropertyMetadata(null));

        public new Brush Foreground
        {
            get => (Brush)GetValue(ForegroundProperty);
            set => SetValue(ForegroundProperty, value);
        }

        public new static readonly DependencyProperty ForegroundProperty =
        DependencyProperty.Register(
            name: "Foreground",
            propertyType: typeof(Brush),
            ownerType: typeof(ToolbarButton),
            typeMetadata: new FrameworkPropertyMetadata(Brushes.WhiteSmoke));


        public PackIconKind Kind
        {
            get => (PackIconKind)GetValue(KindProperty);
            set => SetValue(KindProperty, value);
        }

        public static readonly DependencyProperty KindProperty =
        DependencyProperty.Register(
            name: "Kind",
            propertyType: typeof(PackIconKind),
            ownerType: typeof(ToolbarButton),
            typeMetadata: new FrameworkPropertyMetadata(PackIconKind.Waveform));

        public ToolbarButton()
        {
            InitializeComponent();
        }
    }
}
