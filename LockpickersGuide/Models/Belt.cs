using LockpickersGuide.Logic;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace LockpickersGuide.Models
{
    public sealed class Belt : IModelItem, IEquatable<Belt>
    {
        public int DatabaseId { get; set; }
        public string Name { get; set; }
        public string Imagelink { get; set; }
        public string Description { get; set; }

        public bool Equals(Belt other)
        {
            return this.DatabaseId == other.DatabaseId &&
                this.Name == other.Name &&
                this.Imagelink == other.Imagelink &&
                this.Description == other.Description;
        }

        public bool Update()
        {
            return Database.UpdateBelt(this);
        }
        public int Insert()
        {
            return Database.AddBelt(this);
        }
        public bool Delete()
        {
            return Database.DeleteBelt(this);
        }
    }

    public class BeltComparer : IEqualityComparer<Belt>
    {
        public bool Equals(Belt x, Belt y)
        {
            return x.DatabaseId == y.DatabaseId &&
                x.Name == y.Name &&
                x.Imagelink == y.Imagelink &&
                x.Description == y.Description;
        }

        public int GetHashCode([DisallowNull] Belt obj)
        {
            return obj.DatabaseId.GetHashCode() ^
                (obj.Name == null ? 0 : obj.Name.GetHashCode()) ^
                (obj.Imagelink == null ? 0 : obj.Imagelink.GetHashCode()) ^
                (obj.Description == null ? 0 : obj.Description.GetHashCode());
        }
    }
}
