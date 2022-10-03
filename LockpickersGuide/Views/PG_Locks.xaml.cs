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
using MaterialDesignThemes.Wpf;
using LockpickersGuide.ViewModels;
using LockpickersGuide.ViewLogic;
using System.Reflection;

namespace LockpickersGuide.Views
{
    /// <summary>
    /// Interaction logic for PG_Locks.xaml
    /// </summary>
    public partial class PG_Locks : LockpickerPage
    {
        public PG_Locks()
        {
            InitializeComponent();
            base.DataContext = new PG_LocksViewModel();
            base.Pagename = "Locks";
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

        private void BTN_Add_Click(object sender, RoutedEventArgs e)
        {
            Lock l = new();

            CrudEdit c = new(l, new string[] { "databaseid" })
            {
                Owner = Window.GetWindow(this)
            };

            c.ShowDialog();

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

            string windowContent = "Are you sure you want to delete this item?";

            PropertyInfo s = this.DGV_Main.SelectedItem.GetType().GetProperty("Name");

            if (s.IsSet())
            {
                windowContent = $"Are you sure you want to delete \"{s.GetValue(this.DGV_Main.SelectedItem)}\" this item?";
            }

            WND_DialogBox w = new(WND_DialogBox.DialogStyles.YesNo, windowContent, PackIconKind.Delete)
            {
                Owner = Window.GetWindow(this)
            };

            w.ShowDialog();

            if (w.BoxDialogResult == WND_DialogBox.BoxDialogResults.No)
            {
                return;
            }
        }

        private OV_Filter filterWindow = null;
        private void BTN_Filter_Click(object sender, RoutedEventArgs e)
        {
            //this.MainOpac = 0.1f;
            //this.OnPropertyChanged(nameof(this.MainOpac));

            //MainWindow.Instance.Opac = 0.1f;
            //MainWindow.ViewModelInstance.GreyOut = true;

            FilterOption[] filterOptions =
            {
                new FilterOption()
                {
                    Name = "Brand",
                    Kind = FilterOption.Kinds.Combobox,
                    ComboboxItems = ObjectStorage.Brands.ToArray(),
                    Type = typeof(Brand)
                },
                new FilterOption()
                {
                    Name = "Core",
                    Kind = FilterOption.Kinds.Combobox,
                    ComboboxItems = ObjectStorage.Cores.ToArray(),
                    Type = typeof(Core)
                },
                new FilterOption()
                {
                    Name = "Picked",
                    Kind = FilterOption.Kinds.Bool,
                    Type = typeof(bool)
                },
                new FilterOption()
                {
                    Name = "Owned",
                    Kind = FilterOption.Kinds.Bool,
                    Type = typeof(bool)
                },
                new FilterOption()
                {
                    Name = "Model",
                    Kind = FilterOption.Kinds.Text,
                    Type = typeof(string)
                }
            };

            if (!this.filterWindow.IsSet())
            {
                this.filterWindow = new OV_Filter("Additional filters", Window.GetWindow(this), this, filterOptions);
            }

            this.filterWindow.ShowDialog();
            ((PG_LocksViewModel)this.DataContext).FilterResults = this.filterWindow.FilterResults;


            //MainWindow.ViewModelInstance.GreyOut = false;

            //MainWindow.Instance.Opac = 1.0f;
            //this.MainOpac = 1.0f;
            //this.OnPropertyChanged(nameof(this.MainOpac));
        }
    }
}
