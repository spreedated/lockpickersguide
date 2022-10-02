#pragma warning disable CA1822

using LockpickersGuide.ViewLogic;
using LockpickersGuide.Views;
using MaterialDesignThemes.Wpf;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace LockpickersGuide.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public string Title
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyTitleAttribute>().Title;
            }
        }
        private bool _ShowLoadingFrame = true;
        public bool ShowLoadingFrame
        {
            get
            {
                return this._ShowLoadingFrame;
            }
            set
            {
                this._ShowLoadingFrame = value;
                base.OnPropertyChanged(nameof(this.ShowLoadingFrame));
            }
        }
        private bool _ShowContent;
        public bool ShowContent
        {
            get
            {
                return this._ShowContent;
            }
            set
            {
                this._ShowContent = value;
                base.OnPropertyChanged(nameof(this.ShowContent));
            }
        }
        private PackIconKind _ToolbarMaximizeIcon = PackIconKind.Fullscreen;
        public PackIconKind ToolbarMaximizeIcon
        {
            get
            {
                return this._ToolbarMaximizeIcon;
            }
            set
            {
                this._ToolbarMaximizeIcon = value;
                base.OnPropertyChanged(nameof(this.ToolbarMaximizeIcon));
            }
        }

        public ICommand WindowClose
        {
            get
            {
                return new RelayCommand((sender) =>
                {
                    WND_DialogBox d = new(WND_DialogBox.DialogStyles.YesNo, "Are you sure you want to exit?", PackIconKind.Cross)
                    {
                        Owner = (Window)sender,
                        DialogIconForeground = (SolidColorBrush)new BrushConverter().ConvertFrom("#550000")
                    };
                    d.ShowDialog();
                    if (d.BoxDialogResult == WND_DialogBox.BoxDialogResults.Yes)
                    {
                        ((Window)sender).Close();
                    }
                });
            }
        }
        public ICommand WindowMaximize
        {
            get
            {
                return new RelayCommand((sender) =>
                {
                    Window w = (Window)sender;
                    WindowState s = w.WindowState;
                    if (s == WindowState.Normal)
                    {
                        this.ToolbarMaximizeIcon = PackIconKind.FullscreenExit;
                        w.WindowState = WindowState.Maximized;
                    }
                    if (s == WindowState.Maximized)
                    {
                        this.ToolbarMaximizeIcon = PackIconKind.Fullscreen;
                        w.WindowState = WindowState.Normal;
                    }
                });
            }
        }
        public ICommand WindowMinimize
        {
            get
            {
                return new RelayCommand((sender) => { ((Window)sender).WindowState = WindowState.Minimized; });
            }
        }
        public ICommand ToolbarOpenGithub
        {
            get
            {
                return new RelayCommand((sender) => { Process.Start(new ProcessStartInfo("https://github.com/spreedated/lockpickersguide") { UseShellExecute = true }); });
            }
        }
    }
}
