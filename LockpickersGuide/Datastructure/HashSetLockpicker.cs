using LockpickersGuide.Models;
using Serilog;
using System.Collections.Generic;

namespace LockpickersGuide.Datastructure
{
    public class HashSetLockpicker<T> : HashSet<T>
    {
        public HashSetLockpicker() : base()
        {

        }

        public HashSetLockpicker(IEqualityComparer<T> comparer) : base(comparer)
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
