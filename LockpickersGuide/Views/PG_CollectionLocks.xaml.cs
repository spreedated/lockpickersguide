using LockpickersGuide.Datastructure;
using LockpickersGuide.Logic;
using LockpickersGuide.Models;
using LockpickersGuide.PresentiationModels;
using Serilog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
    /// Interaction logic for PG_Main.xaml
    /// </summary>
    public partial class PG_CollectionLocks : LockpickerPage, INotifyPropertyChanged
    {
        public ObservableCollection<Brand> Brands { get; internal set; }
        public ObservableCollection<CollectionLocks> CollectionLocks { get; internal set; }
        public PG_CollectionLocks()
        {
            InitializeComponent();
            this.DataContext = this;
            base.Pagename = "CollectionLocks";

            Brands = new(ObjectStorage.Brands);
            Brands.Insert(0, new Brand() { Name = "All" });
            Brands.Insert(1, new Brand() { Name = "---" });

            CollectionLocks = new(Database.GetCollectionLocks());
        }

        private void CMB_Brands_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Brand b = (Brand)((ComboBox)sender).SelectedItem;

            if (b == null)
            {
                return;
            }

            if (b.Name == "All" && b.DatabaseId == 0)
            {
                DGV_Main.ItemsSource = CollectionLocks;
                this.SetBindingsOnDGVHeaders();
                return;
            }

            if (b.Name == "---" && b.DatabaseId == 0)
            {
                return;
            }

            DGV_Main.ItemsSource = CollectionLocks.Where(x => x.Brand == b);

            this.SetBindingsOnDGVHeaders();
        }

        private void SetBindingsOnDGVHeaders()
        {
            if (DGV_Main.HasItems)
            {
                ((DataGridTextColumn)DGV_Main.Columns.FirstOrDefault(x => x.Header.ToString() == "Brand")).Binding = new Binding("Brand.Name");
                ((DataGridTextColumn)DGV_Main.Columns.FirstOrDefault(x => x.Header.ToString() == "Type")).Binding = new Binding("Type.Name");
                ((DataGridTextColumn)DGV_Main.Columns.FirstOrDefault(x => x.Header.ToString() == "Core")).Binding = new Binding("Core.Name");
            }
            LBL_Rowcount.Content = $"{DGV_Main.Items.Count} Lock{(DGV_Main.Items.Count == 1 ? "" : "s")}";
        }

        #region WPF Stuff
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public event NotifyCollectionChangedEventHandler CollectionChanged = delegate { };
        public void OnCollectionChanged(NotifyCollectionChangedAction action)
        {
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(action));
        }
        #endregion

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (base.firstload)
            {
                Log.Debug($"[View][PG_Brands] First load");

                base.firstload ^= true;
            }
        }
    }
}
