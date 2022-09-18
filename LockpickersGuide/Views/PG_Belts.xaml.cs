﻿using LockpickersGuide.Datastructure;
using LockpickersGuide.Logic;
using LockpickersGuide.Models;
using LockpickersGuide.PresentiationModels;
using Serilog;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace LockpickersGuide.Views
{
    /// <summary>
    /// Interaction logic for PG_Belts.xaml
    /// </summary>
    public partial class PG_Belts : LockpickerPage, INotifyPropertyChanged
    {
        public ObservableCollection<Belt> ComboboxBelts { get; internal set; }
        public ObservableCollection<Belt> Belts { get; internal set; }
        public ObservableCollection<Belt> DatagridBelts { get; internal set; }
        public Belt DummyBelt
        {
            get
            {
                return new()
                {
                    Name = "Nothing"
                };
            }
        }
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

                this.Belts = new(ObjectStorage.Belts);
                OnPropertyChanged(nameof(this.Belts));

                this.DatagridBelts = new(ObjectStorage.Belts);
                OnPropertyChanged(nameof(this.DatagridBelts));

                this.ComboboxBelts = new(ObjectStorage.Belts);
                this.ComboboxBelts.Insert(0, new() { Name = "Select Belt" });
                this.ComboboxBelts.Insert(1, new() { Name = "---" });
                OnPropertyChanged(nameof(this.ComboboxBelts));

                base.firstload ^= true;
            }
        }

        private void CMB_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (this.CMB_Sortby.SelectedIndex <= 1)
            {
                this.DatagridBelts = new(ObjectStorage.Belts);
                OnPropertyChanged(nameof(this.DatagridBelts));
                this.CMB_Sortby.SelectedIndex = -1;
                return;
            }
            this.DatagridBelts = new(ObjectStorage.Belts.Where(x => x.Name == ((Belt)CMB_Sortby.SelectedItem).Name));
            OnPropertyChanged(nameof(this.DatagridBelts));
        }

        private void BTN_Reset_Click(object sender, RoutedEventArgs e)
        {
            this.TXT_Searchtext.Text = null;
            this.DatagridBelts = new(ObjectStorage.Belts);
            OnPropertyChanged(nameof(this.DatagridBelts));
            this.CMB_Sortby.SelectedIndex = -1;
        }

        private void BTN_Search_Click(object sender, RoutedEventArgs e)
        {
            this.DatagridBelts = new(ObjectStorage.Belts.Where(x=>x.Name.ToLower().Contains(TXT_Searchtext.Text.ToLower()) || x.Description.ToLower().Contains(TXT_Searchtext.Text.ToLower())));
            OnPropertyChanged(nameof(this.DatagridBelts));
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
    }
}
