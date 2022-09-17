using LockpickersGuide.Logic;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace LockpickersGuide
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            this.Icon = new BitmapImage(new Uri("pack://application:,,,/LockpickersGuide;component/Ressources/2472420.png"));

            Preload.PreloadComplete += (o, e) => { this.Dispatcher.Invoke(async () => { await Task.Delay(1500); FRM_Main.Navigate(new Uri("Views\\PG_Main.xaml", UriKind.Relative)); }); };
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
