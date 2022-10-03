using LockpickersGuide.Logic;
using MaterialDesignThemes.Wpf;
using LockpickersGuide.ViewLogic;
using LockpickersGuide.ViewModels;
using LockpickersGuide.Views.Elements;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using LockpickersGuide.Views.UC;

namespace LockpickersGuide.Views
{
    /// <summary>
    /// Interaction logic for OV_Filter.xaml
    /// </summary>
    public partial class OV_Filter : AdvancedWindow
    {
        public IEnumerable<FilterOption> FilterResults { get; }

        private Page callingPage;
        public OV_Filter(string windowName, Window window = null, Page page = null, IEnumerable<FilterOption> filterOptions = null)
        {
            InitializeComponent();
            this.DataContext = new OV_FilterViewModel()
            {
                WindowTitle = windowName
            };
            this.Owner = window;
            this.callingPage = page;

            foreach (FilterOption f in filterOptions)
            {
                this.WRP_Buttons.Children.Add(new PictureButton()
                {
                    ButtonText = f.Name,
                    IconKind = PackIconKind.PlusBold,
                    Command = GenerateFilterButtonCommand(f)
                });
            }
        }

        private ICommand GenerateFilterButtonCommand(FilterOption f)
        {
            return new RelayCommand((sender) =>
            {
                if (this.PNL_Elements.Children.OfType<UserControl>().Any(x => x.Name == f.Name))
                {
                    this.PNL_Elements.Children.OfType<UC_FilterOption>().FirstOrDefault(x => x.Name == f.Name).Visibility = Visibility.Visible;
                }
                else
                {
                    this.PNL_Elements.Children.Add(new UC_FilterOption()
                    {
                        Name = f.Name,
                        Labeltext = f.Name,
                        FilterKind = f.Kind,
                        Command = new RelayCommand((sender) =>
                        {
                            this.WRP_Buttons.Children.OfType<PictureButton>().FirstOrDefault(x => x.ButtonText == f.Name).Visibility = Visibility.Visible;
                            if (this.PNL_Elements.Children.OfType<UC_FilterOption>().Any(x => x.Name == f.Name))
                            {
                                this.PNL_Elements.Children.OfType<UC_FilterOption>().FirstOrDefault(x => x.Name == f.Name).Visibility = Visibility.Collapsed;
                            }
                        }),
                        ComboboxItemSource = f.ComboboxItems
                    });
                }

                this.WRP_Buttons.Children.OfType<PictureButton>().FirstOrDefault(x => x.ButtonText == f.Name).Visibility = Visibility.Collapsed;
            });
        }

        internal void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                ((Window)sender).DragMove();
            }
        }

        private void BTN_Apply_Click(object sender, RoutedEventArgs e)
        {
            this.CheckFilterApplication();
        }

        private void BTN_Close_Click(object sender, RoutedEventArgs e)
        {
            this.CheckFilterApplication();
            this.Hide();
        }

        private void CheckFilterApplication()
        {
            if (this.callingPage.IsSet() && this.callingPage.DataContext is IFilter)
            {
                ((IFilter)this.callingPage.DataContext).FilterEnabled = this.PNL_Elements.Children.OfType<UIElement>().Any(x => x.Visibility == Visibility.Visible);
            }
        }
    }
}
