
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace LockpickersGuide.Models
{
    public sealed class Country : IModelItem, IEquatable<Country>
    {
        public int DatabaseId { get; set; }
        public string Iso { get; set; }
        public string Iso3 { get; set; }
        public string Name { get; set; }
        public string Nicename { get; set; }

        public bool Equals(Country other)
        {
            return this.DatabaseId == other.DatabaseId &&
                this.Iso == other.Iso &&
                this.Iso3 == other.Iso3 &&
                this.Name == other.Name &&
                this.Nicename == other.Nicename;
        }
    }

    public class CountryComparer : IEqualityComparer<Country>
    {
        public bool Equals(Country x, Country y)
        {
            return x.DatabaseId == y.DatabaseId &&
                x.Name == y.Name &&
                x.Iso == y.Iso &&
                x.Iso3 == y.Iso3 &&
                x.Nicename == y.Nicename;
        }

        public int GetHashCode([DisallowNull] Country obj)
        {
            return obj.DatabaseId.GetHashCode() ^
                obj.Name.GetHashCode() ^
                obj.Iso.GetHashCode() ^
                obj.Iso3.GetHashCode() ^
                obj.Nicename.GetHashCode();
        }
    }
}
