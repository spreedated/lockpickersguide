using LockpickersGuide.Datastructure;
using LockpickersGuide.Logic;
using LockpickersGuide.Models;
using LockpickersGuide.PresentiationModels;
using Serilog;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using static LockpickersGuide.Logic.Constants;

namespace LockpickersGuide.Views
{
    /// <summary>
    /// Interaction logic for PG_Locks.xaml
    /// </summary>
    public partial class PG_Locks : LockpickerPage, INotifyPropertyChanged
    {
        public ObservableCollection<Belt> ComboboxLocks { get; internal set; }
        public ObservableCollection<Belt> Locks { get; internal set; }
        public ObservableCollection<Belt> DatagridLocks { get; internal set; }
        public PG_Locks()
        {
            InitializeComponent();
            base.DataContext = this;
            base.Pagename = "Locks";
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

                //this.Locks = new(ObjectStorage.Locks);
                //OnPropertyChanged(nameof(this.Locks));

                //this.DatagridLocks = new(ObjectStorage.Locks);
                //OnPropertyChanged(nameof(this.DatagridLocks));

                //this.ComboboxLocks = new(ObjectStorage.Locks);
                //this.ComboboxLocks.Insert(0, new() { Name = "Select Belt" });
                //this.ComboboxLocks.Insert(1, new() { Name = "---" });
                //OnPropertyChanged(nameof(this.ComboboxLocks));

                base.firstload ^= true;
            }
        }

        private void CMB_SelectionChanged(object sender, RoutedEventArgs e)
        {
            //if (this.CMB_Sortby.SelectedIndex <= 1)
            //{
            //    this.DatagridLocks = new(ObjectStorage.Locks);
            //    OnPropertyChanged(nameof(this.DatagridLocks));
            //    this.CMB_Sortby.SelectedIndex = -1;
            //    return;
            //}
            //this.DatagridLocks = new(ObjectStorage.Locks.Where(x => x.Name == ((Belt)CMB_Sortby.SelectedItem).Name));
            //OnPropertyChanged(nameof(this.DatagridLocks));
        }

        private void BTN_Reset_Click(object sender, RoutedEventArgs e)
        {
            //this.TXT_Searchtext.Text = null;
            //this.DatagridLocks = new(ObjectStorage.Locks);
            //OnPropertyChanged(nameof(this.DatagridLocks));
            //this.CMB_Sortby.SelectedIndex = -1;
        }

        private void BTN_Search_Click(object sender, RoutedEventArgs e)
        {
            //this.DatagridLocks = new(ObjectStorage.Locks.Where(x=>x.Name.ToLower().Contains(TXT_Searchtext.Text.ToLower()) || x.Description.ToLower().Contains(TXT_Searchtext.Text.ToLower())));
            //OnPropertyChanged(nameof(this.DatagridLocks));
        }

        private void TXT_Searchbox_OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                this.BTN_Search_Click(this, null);
            }
        }

        private void TXT_Searchbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (((TextBox)sender).Text.Length <= 0)
            {
                this.BTN_Reset_Click(this, null);
            }
        }

        private void BTN_Add_Click(object sender, RoutedEventArgs e)
        {
            Belt b = new();

            CrudEdit c = new(b, new string[] { "databaseid" })
            {
                Owner = Window.GetWindow(this)
            };
            //if ((bool)c.ShowDialog())
            //{
            //    b = (Belt)c.Object;
            //    b.Insert();

            //    Logic.Options.Instance.ForceDatabaseReload = true;
            //    Logic.Preload.FillObjectStorage<Belt>(ref ObjectStorage.Locks, () => Database.GetLocks(), CACHE_Locks);

            //    this.Locks = new(ObjectStorage.Locks);
            //    OnPropertyChanged(nameof(this.Locks));

            //    this.DatagridLocks = new(ObjectStorage.Locks);
            //    OnPropertyChanged(nameof(this.DatagridLocks));

            //    this.ComboboxLocks = new(ObjectStorage.Locks);
            //    this.ComboboxLocks.Insert(0, new() { Name = "Select Belt" });
            //    this.ComboboxLocks.Insert(1, new() { Name = "---" });
            //    OnPropertyChanged(nameof(this.ComboboxLocks));

            //    Logic.Options.Instance.ForceDatabaseReload = true;
            //}

        }

        private void BTN_Edit_Click(object sender, RoutedEventArgs e)
        {
            if (this.DGV_Main.SelectedIndex <= -1)
            {
                return;
            }

            //Belt b = (Belt)this.DGV_Main.SelectedItem;

            //CrudEdit c = new(b, new string[] { "databaseid" })
            //{
            //    Owner = Window.GetWindow(this)
            //};
            //if ((bool)c.ShowDialog())
            //{
            //    b = (Belt)c.Object;
            //    b.Update();

            //    Logic.Options.Instance.ForceDatabaseReload = true;
            //    Logic.Preload.FillObjectStorage<Belt>(ref ObjectStorage.Locks, () => Database.GetLocks(), CACHE_Locks);

            //    this.Locks = new(ObjectStorage.Locks);
            //    OnPropertyChanged(nameof(this.Locks));

            //    this.DatagridLocks = new(ObjectStorage.Locks);
            //    OnPropertyChanged(nameof(this.DatagridLocks));

            //    this.ComboboxLocks = new(ObjectStorage.Locks);
            //    this.ComboboxLocks.Insert(0, new() { Name = "Select Belt" });
            //    this.ComboboxLocks.Insert(1, new() { Name = "---" });
            //    OnPropertyChanged(nameof(this.ComboboxLocks));

            //    Logic.Options.Instance.ForceDatabaseReload = true;
            //}
        }

        private void BTN_Delete_Click(object sender, RoutedEventArgs e)
        {
            if (this.DGV_Main.SelectedIndex <= -1)
            {
                return;
            }

            WND_DialogBox w = new(WND_DialogBox.DialogStyles.YesNo, $"Are you sure you want to delete Belt \"{((Belt)this.DGV_Main.SelectedItem).Name}\"?", MahApps.Metro.IconPacks.PackIconMaterialDesignKind.Delete)
            {
                Owner = Window.GetWindow(this)
            };
            w.ShowDialog();

            if (w.BoxDialogResult == WND_DialogBox.BoxDialogResults.No)
            {
                return;
            }

            //Belt b = (Belt)this.DGV_Main.SelectedItem;
            //b.Delete();

            //Logic.Options.Instance.ForceDatabaseReload = true;
            //ObjectStorage.Locks.Clear();
            //Logic.Preload.FillObjectStorage<Belt>(ref ObjectStorage.Locks, () => Database.GetLocks(), CACHE_Locks);
            //Logic.Options.Instance.ForceDatabaseReload = false;

            //this.Locks = new(ObjectStorage.Locks);
            //OnPropertyChanged(nameof(this.Locks));

            //this.DatagridLocks = new(ObjectStorage.Locks);
            //OnPropertyChanged(nameof(this.DatagridLocks));

            //this.ComboboxLocks = new(ObjectStorage.Locks);
            //this.ComboboxLocks.Insert(0, new() { Name = "Select Belt" });
            //this.ComboboxLocks.Insert(1, new() { Name = "---" });
            //OnPropertyChanged(nameof(this.ComboboxLocks));
        }

        private void BTN_Filter_Click(object sender, RoutedEventArgs e)
        {
            //this.MainOpac = 0.1f;
            //this.OnPropertyChanged(nameof(this.MainOpac));

            //MainWindow.Instance.Opac = 0.1f;
            MainWindow.ViewModelInstance.GreyOut(true);

            new OV_Filter("Filters") { Owner = Window.GetWindow(this) }.ShowDialog();

            MainWindow.ViewModelInstance.GreyOut(false);

            //MainWindow.Instance.Opac = 1.0f;
            //this.MainOpac = 1.0f;
            //this.OnPropertyChanged(nameof(this.MainOpac));
        }
    }
}
