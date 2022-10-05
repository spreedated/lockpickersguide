using LockpickersGuide.ViewLogic;
using LockpickersGuide.Views.UC;
using MahApps.Metro.IconPacks;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using static LockpickersGuide.Views.WND_DialogBox;

namespace LockpickersGuide.Views
{
    /// <summary>
    /// Interaction logic for WND_DialogBox.xaml
    /// </summary>
    public partial class WND_DialogBox : AdvancedWindow, INotifyPropertyChanged
    {
        public Brush DialogIconForeground { get; set; } = Brushes.WhiteSmoke;
        public Visibility YesNoDialog { get; private set; } = Visibility.Collapsed;
        public Visibility OkayOnlyDialog { get; private set; } = Visibility.Collapsed;
        public BoxDialogResults BoxDialogResult { get; private set; }
        public PackIconKind DialogIconMaterial { get; private set; } = PackIconKind.None;
        public Visibility DialogIconMaterialVisibility
        {
            get
            {
                return this.DialogIconMaterial == PackIconKind.None ? Visibility.Collapsed : Visibility.Visible;
            }
        }
        private PackIconVaadinIconsKind _DialogIconVaadin = PackIconVaadinIconsKind.None;
        public PackIconVaadinIconsKind DialogIconVaadin
        {
            get
            {
                return this._DialogIconVaadin;
            }
            private set
            {
                this._DialogIconVaadin = value;
                this.OnPropertyChanged(nameof(this.DialogIconVaadin));
            }
        }
        public IEnumerable<PackIconVaadinIconsKind> DialogIconsVaadin { get; private set; }
        private Visibility _DialogIconVaadinVisibility = Visibility.Collapsed;
        public Visibility DialogIconVaadinVisibility
        {
            get
            {
                return this._DialogIconVaadinVisibility;
            }
            set
            {
                this._DialogIconVaadinVisibility = value;
                this.OnPropertyChanged(nameof(this.DialogIconVaadinVisibility));
            }
        }
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

        private readonly Timer animationTimer = new();

        public WND_DialogBox(DialogStyles dialogStyle, string dialogText, PackIconKind icon = PackIconKind.None)
        {
            this.Initialize(dialogStyle, dialogText);

            if (icon != PackIconKind.None)
            {
                this.DialogIconVisibility = Visibility.Visible;
                this.DialogIconMaterial = icon;
            }
        }

        public WND_DialogBox(DialogStyles dialogStyle, string dialogText, PackIconVaadinIconsKind icon = PackIconVaadinIconsKind.None)
        {
            this.Initialize(dialogStyle, dialogText);

            if (icon != PackIconVaadinIconsKind.None)
            {
                this.DialogIconVisibility = Visibility.Visible;
                this.DialogIconVaadinVisibility = Visibility.Visible;
                this.DialogIconVaadin = icon;
            }
        }

        public WND_DialogBox(DialogStyles dialogStyle, string dialogText, IEnumerable<PackIconVaadinIconsKind> icons)
        {
            this.Initialize(dialogStyle, dialogText);

            this.DialogIconsVaadin = icons;
            this.DialogIconVaadin = this.DialogIconsVaadin.ToArray()[0];
            this.DialogIconVaadinVisibility = Visibility.Visible;
            this.DialogIconVisibility = Visibility.Visible;

            this.animationTimer.Enabled = true;
            this.animationTimer.Start();
        }

        private void Initialize(DialogStyles dialogStyle, string dialogText)
        {
            InitializeComponent();
            this.DataContext = this;

            ChooseStyle(dialogStyle);
            this.DialogText = dialogText;

            this.animationTimer.Interval = 250;
            this.animationTimer.Elapsed += this.AnimationTimer_Elapsed;
        }

        private short AnimationStatus = 0;
        private void AnimationTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                this.DialogIconVaadin = this.DialogIconsVaadin.ToArray()[this.AnimationStatus];
            });
            this.AnimationStatus++;
            if (this.AnimationStatus >= this.DialogIconsVaadin.Count())
            {
                this.AnimationStatus = 0;
            }
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
