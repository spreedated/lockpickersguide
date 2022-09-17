using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace LockpickersGuide.Models
{
    public sealed class Color : IModelItem, IEquatable<Color>
    {
        public int DatabaseId { get; set; }
        public string Name { get; set; }

        public bool Equals(Color other)
        {
            return this.DatabaseId == other.DatabaseId &&
                this.Name == other.Name;
        }
    }

    public class ColorComparer : IEqualityComparer<Color>
    {
        public bool Equals(Color x, Color y)
        {
            return x.DatabaseId == y.DatabaseId &&
                x.Name == y.Name;
        }

        public int GetHashCode([DisallowNull] Color obj)
        {
            return obj.DatabaseId.GetHashCode() ^
                (obj.Name == null ? 0 : obj.Name.GetHashCode());
        }
    }
}
