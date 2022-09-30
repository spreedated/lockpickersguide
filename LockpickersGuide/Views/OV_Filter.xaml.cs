using LockpickersGuide.ViewLogic;
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

namespace LockpickersGuide.Views
{
    /// <summary>
    /// Interaction logic for OV_Filter.xaml
    /// </summary>
    public partial class OV_Filter : AdvancedWindow, INotifyPropertyChanged
    {
        public string WindowName { get; set; }

        public OV_Filter(string windowName)
        {
            InitializeComponent();
            this.DataContext = this;
            this.WindowName = windowName;

            this.OnPropertyChanged(nameof(this.WindowName));
        }

        internal void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                ((Window)sender).DragMove();
            }
        }

        private void BTN_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
