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
        private string _WindowTitle;
        public string WindowTitle
        {
            get
            {
                return this._WindowTitle;
            }
            set
            {
                this._WindowTitle = value;
                base.OnPropertyChanged(nameof(this.WindowTitle));
            }
        }
    }
}
