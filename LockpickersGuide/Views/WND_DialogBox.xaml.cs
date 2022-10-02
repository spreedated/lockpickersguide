using LockpickersGuide.ViewLogic;
using MahApps.Metro.IconPacks;
using MaterialDesignThemes.Wpf;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LockpickersGuide.Views
{
    /// <summary>
    /// Interaction logic for WND_DialogBox.xaml
    /// </summary>
    public partial class WND_DialogBox : AdvancedWindow, INotifyPropertyChanged
    {
        public Visibility YesNoDialog { get; private set; } = Visibility.Collapsed;
        public Visibility OkayOnlyDialog { get; private set; } = Visibility.Collapsed;
        public BoxDialogResults BoxDialogResult { get; private set; }
        public PackIconKind DialogIcon { get; private set; } = PackIconKind.None;
        public Visibility DialogIconVisibility { get; private set; } = Visibility.Collapsed;
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

        public WND_DialogBox(DialogStyles dialogStyle, string dialogText, PackIconKind icon = PackIconKind.None)
        {
            InitializeComponent();
            this.DataContext = this;

            if (icon != PackIconKind.None)
            {
                this.DialogIconVisibility = Visibility.Visible;
                this.DialogIcon = icon;
            }

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
