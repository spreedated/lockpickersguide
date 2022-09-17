using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace LockpickersGuide.Models
{
    public sealed class Locktype : IModelItem, IEquatable<Locktype>
    {
        public int DatabaseId { get; set; }
        public string Name { get; set; }

        public bool Equals(Locktype other)
        {
            return this.DatabaseId == other.DatabaseId &&
                this.Name == other.Name;
        }
    }

    public class LocktypeComparer : IEqualityComparer<Locktype>
    {
        public bool Equals(Locktype x, Locktype y)
        {
            return x.DatabaseId == y.DatabaseId &&
                x.Name == y.Name;
        }

        public int GetHashCode([DisallowNull] Locktype obj)
        {
            return obj.DatabaseId.GetHashCode() ^
                (obj.Name == null ? 0 : obj.Name.GetHashCode());
        }
    }
}
