
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace LockpickersGuide.Models
{
    public sealed class Brand : IModelItem, IEquatable<Brand>
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
                (this.Country == other.Country || this.Country.Equals(other.Country)) &&
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
            if (x == null || y == null)
            {
                return false;
            }

            return x.DatabaseId == y.DatabaseId &&
                x.Name == y.Name &&
                (x.Country == y.Country || x.Country.Equals(y.Country)) &&
                x.Founded == y.Founded &&
                x.City == y.City &&
                x.Website == y.Website &&
                x.Website == y.Website &&
                x.AltName == y.AltName &&
                x.Description == y.Description;
        }

        public int GetHashCode([DisallowNull] Brand obj)
        {
            if (obj == null)
            {
                return -1;
            }

            return obj.DatabaseId.GetHashCode() ^
                (obj.Name == null ? 0 : obj.Name.GetHashCode()) ^
                (obj.Founded == null ? 0 : obj.Founded.GetHashCode()) ^
                (obj.City == null ? 0 : obj.City.GetHashCode()) ^
                (obj.Website == null ? 0 : obj.Website.GetHashCode()) ^
                (obj.AltName == null ? 0 : obj.AltName.GetHashCode()) ^
                (obj.Description == null ? 0 : obj.Description.GetHashCode());
        }
    }
}
