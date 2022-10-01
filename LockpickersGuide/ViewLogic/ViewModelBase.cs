﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;

namespace LockpickersGuide.ViewLogic
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private float _Opac = 1.0f;
        public float Opac
        {
            get
            {
                return _Opac;
            }
            set
            {
                _Opac = value;
                this.OnPropertyChanged(nameof(this.Opac));
            }
        }

        private Visibility _GreyOutVisibility = Visibility.Collapsed;
        public Visibility GreyOutVisibility
        {
            get
            {
                return _GreyOutVisibility;
            }
            set
            {
                _GreyOutVisibility = value;
                this.OnPropertyChanged(nameof(this.GreyOutVisibility));
            }
        }

        public void GreyOut(bool visible = true)
        {
            this.GreyOutVisibility = visible ? Visibility.Visible : Visibility.Collapsed;
        }

        public static IEnumerable<T> FindVisualChilds<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj == null) yield return (T)Enumerable.Empty<T>();
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                DependencyObject ithChild = VisualTreeHelper.GetChild(depObj, i);
                if (ithChild == null) continue;
                if (ithChild is T t) yield return t;
                foreach (T childOfChild in FindVisualChilds<T>(ithChild)) yield return childOfChild;
            }
        }
    }
}
