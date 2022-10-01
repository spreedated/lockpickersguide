#pragma warning disable CA1822

using LockpickersGuide.ViewLogic;
using LockpickersGuide.Views;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

        public ICommand WindowClose
        {
            get
            {
                return new RelayCommand((sender) => { ((Window)sender).Close(); });
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
                        FindVisualChilds<Button>(w).FirstOrDefault(x=> x.Name.StartsWith("BTN_Maxi")).Content = "⧉";
                        w.WindowState = WindowState.Maximized;
                    }
                    if (s == WindowState.Maximized)
                    {
                        FindVisualChilds<Button>(w).FirstOrDefault(x => x.Name.StartsWith("BTN_Maxi")).Content = "□";
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
    }
}
