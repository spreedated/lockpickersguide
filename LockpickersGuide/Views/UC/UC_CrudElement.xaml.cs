using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace LockpickersGuide.Views.UC
{
    /// <summary>
    /// Interaction logic for UC_CrudElement.xaml
    /// </summary>
    public partial class UC_CrudElement : UserControl, INotifyPropertyChanged
    {
        private string _ObjectName;
        public string ObjectName
        {
            get
            {
                return this._ObjectName;
            }
            set
            {
                this._ObjectName = value;
                this.OnPropertyChanged(nameof(ObjectName));
            }
        }

        private string _ObjectValue;
        public string ObjectValue
        {
            get
            {
                return this._ObjectValue;
            }
            set
            {
                this._ObjectValue = value;
                this.OnPropertyChanged(nameof(ObjectValue));
            }
        }
        public UC_CrudElement()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
