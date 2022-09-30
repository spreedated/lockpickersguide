using LockpickersGuide.Logic;
using LockpickersGuide.ViewLogic;
using LockpickersGuide.ViewModels;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace LockpickersGuide.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : AdvancedWindow
    {
        public static MainWindowViewModel Instance { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel();
            this.Icon = new BitmapImage(new Uri("pack://application:,,,/LockpickersGuide;component/Ressources/icon_white.png"));
            
            Logic.Preload.PreloadComplete += (o, e) => { this.Dispatcher.Invoke(async () => { await Task.Delay(1500); FRM_Main.Navigate(new Uri("Views\\PG_Main.xaml", UriKind.Relative)); }); };

            Instance = (MainWindowViewModel)this.DataContext;
        }

        internal void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                ((Window)sender).DragMove();
            }
        }
    }
}
