using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace LockpickersGuide.Models
{
    public sealed class Core : IModelItem, IEquatable<Core>
    {
        public int DatabaseId { get; set; }
        public string Name { get; set; }

        public bool Equals(Core other)
        {
            return this.DatabaseId == other.DatabaseId &&
                this.Name == other.Name;
        }
    }

    public class CoreComparer : IEqualityComparer<Core>
    {
        public bool Equals(Core x, Core y)
        {
            return x.DatabaseId == y.DatabaseId &&
                x.Name == y.Name;
        }

        public int GetHashCode([DisallowNull] Core obj)
        {
            return obj.DatabaseId.GetHashCode() ^
                (obj.Name == null ? 0 : obj.Name.GetHashCode());
        }
    }
}
