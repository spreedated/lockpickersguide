using LockpickersGuide.ViewLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LockpickersGuide.ViewModels
{
    public class OV_FilterViewModel : ViewModelBase
    {
        private string _WindowName;
        public string WindowName
        {
            get
            {
                return this._WindowName;
            }
            set
            {
                this._WindowName = value;
                base.OnPropertyChanged(nameof(this.WindowName));
            }
        }
    }
}
