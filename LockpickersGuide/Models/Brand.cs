
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace LockpickersGuide.Models
{
    public class Brand : IModelItem, IEquatable<Brand>
    {
        public int DatabaseId { get; set; }
        public string Name { get; set; }
        public Country Country { get; set; }
        public int? Founded { get; set; }
        public int? FoundedYearsFromNow
        {
            get
            {
                return this.Founded != null ? DateTime.Now.Year - this.Founded : null;
            }
        }
        public string City { get; set; }
        public string Website { get; set; }
        public string AltName { get; set; }
        public string Description { get; set; }

        public bool Equals(Brand other)
        {
            return this.DatabaseId == other.DatabaseId &&
                this.Name == other.Name &&
                this.Country.Equals(other.Country) &&
                this.Founded == other.Founded &&
                this.City == other.City &&
                this.Website == other.Website &&
                this.Website == other.Website &&
                this.AltName == other.AltName &&
                this.Description == other.Description;
        }
    }

    public class BrandComparer : IEqualityComparer<Brand>
    {
        public bool Equals(Brand x, Brand y)
        {
            return x.DatabaseId == y.DatabaseId &&
                x.Name == y.Name &&
                x.Country.Equals(y.Country) &&
                x.Founded == y.Founded &&
                x.City == y.City &&
                x.Website == y.Website &&
                x.Website == y.Website &&
                x.AltName == y.AltName &&
                x.Description == y.Description;
        }

        public int GetHashCode([DisallowNull] Brand obj)
        {
            return obj.DatabaseId.GetHashCode() ^
                obj.Name.GetHashCode() ^
                obj.Founded.GetHashCode() ^
                obj.City.GetHashCode() ^
                obj.Website.GetHashCode() ^
                obj.Website.GetHashCode() ^
                (obj.AltName == null ? 0 : obj.AltName.GetHashCode()) ^
                obj.Description.GetHashCode();
        }
    }
}
