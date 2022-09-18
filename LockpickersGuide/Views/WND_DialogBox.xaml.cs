using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
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
    /// Interaction logic for WND_DialogBox.xaml
    /// </summary>
    public partial class WND_DialogBox : Window, INotifyPropertyChanged
    {
        public Visibility YesNoDialog { get; private set; } = Visibility.Collapsed;
        public Visibility OkayOnlyDialog { get; private set; } = Visibility.Collapsed;
        public BoxDialogResults BoxDialogResult { get; private set; }
        public string DialogText { get; private set; }
        public enum DialogStyles
        {
            OkayOnly = 0,
            YesNo
        }
        public enum BoxDialogResults
        {
            Unknown = 0,
            Yes,
            No,
            Okay
        }
        private void ChooseStyle(DialogStyles dialogStyle)
        {
            switch (dialogStyle)
            {
                case DialogStyles.YesNo:
                    this.YesNoDialog = Visibility.Visible;
                    break;
                case DialogStyles.OkayOnly:
                    this.OkayOnlyDialog = Visibility.Visible;
                    break;
            }
        }

        public WND_DialogBox(DialogStyles dialogStyle, string dialogText)
        {
            InitializeComponent();
            this.DataContext = this;

            ChooseStyle(dialogStyle);
            this.DialogText = dialogText;
        }

        internal void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                ((Window)sender).DragMove();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void BTN_Pressed(object sender, RoutedEventArgs e)
        {
            BoxDialogResults r;

            Enum.TryParse<BoxDialogResults>(((Button)sender).Content.ToString(), out r);

            this.BoxDialogResult = r;
            this.Close();
        }
    }
}
