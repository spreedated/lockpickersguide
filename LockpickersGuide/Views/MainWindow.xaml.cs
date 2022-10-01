using LockpickersGuide.Logic;
using LockpickersGuide.PresentiationModels;
using LockpickersGuide.ViewLogic;
using LockpickersGuide.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace LockpickersGuide.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : AdvancedWindow
    {
        public static MainWindowViewModel ViewModelInstance { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel();
            this.Icon = new BitmapImage(new Uri("pack://application:,,,/LockpickersGuide;component/Ressources/icon_white.png"));
            
            Logic.Preload.PreloadComplete += (o, e) => { this.Dispatcher.Invoke(async () => { await Task.Delay(1500); this.LoadPages(); ViewModelInstance.ShowLoadingFrame = false; ViewModelInstance.ShowContent = true; }); };

            ViewModelInstance = (MainWindowViewModel)this.DataContext;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                ((Window)sender).DragMove();
            }
        }

        private void LoadPages()
        {
            this.pages.Add(new PG_Brands());
            this.pages.Add(new PG_CollectionLocks());
            this.pages.Add(new PG_Belts());
            this.pages.Add(new PG_Locks());

            FRM_Main.Navigate(this.pages.FirstOrDefault(x=>x.Pagename == "CollectionLocks"));
        }

        private readonly List<LockpickerPage> pages = new();

        private void BTN_Brand_Click(object sender, RoutedEventArgs e)
        {
            this.FRM_Main.Navigate(pages.FirstOrDefault(x => x.Pagename.Equals("brands", StringComparison.InvariantCultureIgnoreCase)));
        }

        private void BTN_CollectionLocks_Click(object sender, RoutedEventArgs e)
        {
            this.FRM_Main.Navigate(pages.FirstOrDefault(x => x.Pagename.Equals("collectionLocks", StringComparison.InvariantCultureIgnoreCase)));
        }

        private void BTN_Belts_Click(object sender, RoutedEventArgs e)
        {
            this.FRM_Main.Navigate(pages.FirstOrDefault(x => x.Pagename.Equals("belts", StringComparison.InvariantCultureIgnoreCase)));
        }

        private void BTN_Locks_Click(object sender, RoutedEventArgs e)
        {
            this.FRM_Main.Navigate(pages.FirstOrDefault(x => x.Pagename.Equals("locks", StringComparison.InvariantCultureIgnoreCase)));
        }
    }
}
