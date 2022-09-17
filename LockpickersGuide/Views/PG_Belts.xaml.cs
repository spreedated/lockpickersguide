using LockpickersGuide.Datastructure;
using LockpickersGuide.Logic;
using LockpickersGuide.Models;
using LockpickersGuide.PresentiationModels;
using Serilog;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LockpickersGuide.Views
{
    /// <summary>
    /// Interaction logic for PG_Belts.xaml
    /// </summary>
    public partial class PG_Belts : LockpickerPage, INotifyPropertyChanged
    {
        public HashSetLockpicker<Belt> Belts { get; internal set; }
        public PG_Belts()
        {
            InitializeComponent();
            base.DataContext = this;
            base.Pagename = "Belts";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (base.firstload)
            {
                Log.Debug($"[View][PG_Brands] First load");

                Belts = ObjectStorage.Belts;
                OnPropertyChanged(nameof(this.Belts));

                base.firstload ^= true;
            }
        }
    }
}
