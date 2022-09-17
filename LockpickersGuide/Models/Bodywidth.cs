using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace LockpickersGuide.Models
{
    public sealed class Bodywidth : IModelItem, IEquatable<Bodywidth>
    {
        public int DatabaseId { get; set; }
        public string Name
        {
            get
            {
                return this.Inch;
            }
            set
            {
                _ = value;
            }
        }
        public string Inch { get; set; }
        public double Mm { get; set; }

        public bool Equals(Bodywidth other)
        {
            return this.DatabaseId == other.DatabaseId &&
                this.Inch == other.Inch &&
                this.Mm == other.Mm;
        }
    }

    public class BodywidthComparer : IEqualityComparer<Bodywidth>
    {
        public bool Equals(Bodywidth x, Bodywidth y)
        {
            return x.DatabaseId == y.DatabaseId &&
                x.Inch == y.Inch &&
                x.Mm == y.Mm;
        }

        public int GetHashCode([DisallowNull] Bodywidth obj)
        {
            return obj.DatabaseId.GetHashCode() ^
                (obj.Inch == null ? 0 : obj.Inch.GetHashCode()) ^
                obj.Mm.GetHashCode();
        }
    }
}
