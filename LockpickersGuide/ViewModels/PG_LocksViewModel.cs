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
using System.Windows.Input;

namespace LockpickersGuide.ViewModels
{
    public class PG_LocksViewModel : ViewModelBase, IFilter
    {
        private IEnumerable<FilterOption> _FilterResults;
        public IEnumerable<FilterOption> FilterResults
        {
            get
            {
                return this._FilterResults;
            }
            set
            {
                this._FilterResults = value;
                base.OnPropertyChanged(nameof(this.FilterResults));
            }
        }

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

        public ObservableCollection<Lock> Locks { get; private set; } = new();

        public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand((sender) =>
                {
                    this.Locks.Clear();

                    if (this.FilterResults.IsSet() && this.FilterResults.Any())
                    {
                        foreach (Lock cl in Database.GetLocks(this.FilterResults))
                        {
                            this.Locks.Add(cl);
                        }
                        return;
                    }

                    foreach (Lock cl in Database.GetLocks())
                    {
                        this.Locks.Add(cl);
                    }
                });
            }
        }
    }
}
