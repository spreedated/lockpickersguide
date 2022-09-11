using System.Collections.Generic;
using LockpickersGuide.Models;
using Serilog;

namespace LockpickersGuide.Datastructure
{
    public class HashSetLockpicker<T> : HashSet<T>
    {
        public HashSetLockpicker() : base()
        {

        }

        public new bool Add(T item)
        {
            if (item is IModelItem it)
            {
                Log.Verbose($"[HashSetLockpicker][Add] Item named \"{it.Name}\" added");
            }
            return base.Add(item);
        }
    }
}
