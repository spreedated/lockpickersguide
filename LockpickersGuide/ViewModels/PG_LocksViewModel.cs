#pragma warning disable S101

using LockpickersGuide.Logic;
using LockpickersGuide.Models;
using LockpickersGuide.ViewLogic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LockpickersGuide.ViewModels
{
    public class PG_LocksViewModel : ViewModelBase, IFilter
    {
        private bool _FilterEnabled;
        public bool FilterEnabled
        {
            get
            {
                return this._FilterEnabled;
            }
            set
            {
                this._FilterEnabled = value;
                base.OnPropertyChanged(nameof(this.FilterEnabled));
            }
        }

        public ObservableCollection<Belt> ComboboxLocks { get; internal set; }
        public ObservableCollection<Belt> Locks { get; internal set; }
        public ObservableCollection<Belt> DatagridLocks { get; internal set; }
    }
}
