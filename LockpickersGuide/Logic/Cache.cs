using LockpickersGuide.Datastructure;
using LockpickersGuide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LockpickersGuide.Logic
{
    internal static class Cache
    {
        internal readonly static HashSetLockpicker<Country> Countries = new();
        internal readonly static HashSetLockpicker<Brand> Brands = new();
    }
}
