using LockpickersGuide.Datastructure;
using LockpickersGuide.Logic;
using LockpickersGuide.Models;
using LockpickersGuide.PresentiationModels;
using Serilog;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace LockpickersGuide.Views
{
    /// <summary>
    /// Interaction logic for PG_Main.xaml
    /// </summary>
    public partial class PG_Brands : LockpickerPage, INotifyPropertyChanged
    {
        public HashSetLockpicker<Brand> Brands { get; internal set; }
        public PG_Brands()
        {
            InitializeComponent();
            base.DataContext = this;
            base.Pagename = "Brands";
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

                Brands = ObjectStorage.Brands;
                OnPropertyChanged(nameof(Brands));

                base.firstload ^= true;
            }
        }
    }
}
