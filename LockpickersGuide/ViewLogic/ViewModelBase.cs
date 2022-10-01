using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;

namespace LockpickersGuide.ViewLogic
{
    public class ViewModelBase : INotifyPropertyChanged, IShadow
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

        private bool _GreyOut = false;
        public bool GreyOut
        {
            get
            {
                return _GreyOut;
            }
            set
            {
                _GreyOut = value;
                this.OnPropertyChanged(nameof(this.GreyOut));
            }
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
