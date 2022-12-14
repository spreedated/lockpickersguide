using LockpickersGuide.Datastructure;
using LockpickersGuide.Models;

namespace LockpickersGuide.Logic
{
    internal static class ObjectStorage
    {
        internal static HashSetLockpicker<Country> Countries = new();
        internal static HashSetLockpicker<Brand> Brands = new();
        internal static HashSetLockpicker<Locktype> Locktypes = new();
        internal static HashSetLockpicker<Core> Cores = new();
        internal static HashSetLockpicker<Belt> Belts = new();
        internal static HashSetLockpicker<CollectionLocks> CollectionLocks = new();
        internal static HashSetLockpicker<Bodywidth> Bodywidths = new();
        internal static HashSetLockpicker<Color> Colors = new();
    }
}
