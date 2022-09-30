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
using System.Reflection;
using LockpickersGuide.Views.UC;
using LockpickersGuide.ViewLogic;

namespace LockpickersGuide.Views
{
    /// <summary>
    /// Interaction logic for CrudEdit.xaml
    /// </summary>
    public partial class CrudEdit : AdvancedWindow, INotifyPropertyChanged
    {
        private string _ClassName;
        public string ClassName
        {
            get
            {
                return _ClassName;
            }
            set
            {
                this._ClassName = value;
                this.OnPropertyChanged(nameof(this.ClassName));
            }
        }

        public object Object { get; private set; } = null;
        public CrudEdit(object @object, string[] disallowEditNames = null)
        {
            InitializeComponent();
            this.DataContext = this;

            this.ClassName = @object.GetType().Name;
            this.Object = @object;

            foreach (PropertyInfo p in @object.GetType().GetProperties(BindingFlags.Public |BindingFlags.Instance | BindingFlags.SetProperty))
            {
                if (disallowEditNames.Any(x=> x.ToLower() == p.Name.ToLower()))
                {
                    continue;
                }
                this.PNL_Elements.Children.Add(new UC_CrudElement()
                {
                    ObjectName = p.Name,
                    ObjectValue = p.GetValue(@object)?.ToString(),
                });
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

        private void BTN_Discard_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void BTN_Save_Click(object sender, RoutedEventArgs e)
        {
            foreach (KeyValuePair<string, string> kv in this.PNL_Elements.Children.OfType<UserControl>().Select(x => new KeyValuePair<string, string>(((UC_CrudElement)x).ObjectName,((UC_CrudElement)x).ObjectValue)))
            {
                this.Object.GetType().GetProperty(kv.Key).SetValue(this.Object, kv.Value);
            }
            this.DialogResult = true;
            this.Close();
        }
    }
}
