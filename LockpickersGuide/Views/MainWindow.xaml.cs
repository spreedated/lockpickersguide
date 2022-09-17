using LockpickersGuide.Logic;
using LockpickersGuide.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
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

            Preload.PreloadComplete += (o,e) => { this.Dispatcher.Invoke(async ()=> { await Task.Delay(1500); FRM_Main.Navigate(new Uri("Views\\PG_Main.xaml", UriKind.Relative)); }); };
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
