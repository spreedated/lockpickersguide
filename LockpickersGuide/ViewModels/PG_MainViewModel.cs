using LockpickersGuide.ViewLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LockpickersGuide.ViewModels
{
    public class PG_MainViewModel : ViewModelBase
    {
        public string Title
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyTitleAttribute>().Title;
            }
        }
    }
}
