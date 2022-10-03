using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LockpickersGuide.ViewLogic
{
    public class FilterOption
    {
        public string Name { get; set; }
        public Kinds Kind { get; set; }
        public enum Kinds
        {
            Text = 0,
            Combobox,
            Bool,
            Date
        }
        public object Value { get; set; }
        public object[] ComboboxItems { get; set; }
    }
}
