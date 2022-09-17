using LockpickersGuide.PresentiationModels;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace LockpickersGuide.Views
{
    /// <summary>
    /// Interaction logic for PG_Main.xaml
    /// </summary>
    public partial class PG_Main : Page
    {
        public PG_Main()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        readonly LockpickerPage[] pages = new LockpickerPage[]
        {
            new PG_Brands(),
            new PG_CollectionLocks(),
            new PG_Belts()
        };

        private void BTN_Brand_Click(object sender, RoutedEventArgs e)
        {
            this.MainFrame.Navigate(pages.FirstOrDefault(x => x.Pagename.Equals("brands", StringComparison.InvariantCultureIgnoreCase)));
        }

        private void BTN_CollectionLocks_Click(object sender, RoutedEventArgs e)
        {
            this.MainFrame.Navigate(pages.FirstOrDefault(x => x.Pagename.Equals("collectionLocks", StringComparison.InvariantCultureIgnoreCase)));
        }

        private void BTN_Belts_Click(object sender, RoutedEventArgs e)
        {
            this.MainFrame.Navigate(pages.FirstOrDefault(x => x.Pagename.Equals("belts", StringComparison.InvariantCultureIgnoreCase)));
        }
    }
}
