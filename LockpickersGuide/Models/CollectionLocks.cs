using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace LockpickersGuide.Models
{
    public sealed class CollectionLocks : ICollectionLock, IEquatable<CollectionLocks>, IValidatableObject
    {
        [Required]
        public string DatabaseId { get; set; }
        public string Name
        {
            get
            {
                return this.Model;
            }
        }
        [Required]
        public Locktype Type { get; set; }
        [Required]
        public Brand Brand { get; set; }
        public string Modelname { get; set; }
        public string Model { get; set; }
        public string BindingOrder { get; set; }
        public bool Picked { get; set; }
        [Required]
        public Core Core { get; set; }
        public string Description { get; set; }
        public int Keycount { get; set; }
        public double Price { get; set; }
        public bool Ownership { get; set; }
        public bool Guttable { get; set; }

        public bool Equals(CollectionLocks other)
        {
            return this.DatabaseId == other.DatabaseId &&
                this.Name == other.Name &&
                this.Model == other.Model &&
                this.Modelname == other.Modelname &&
                this.BindingOrder == other.BindingOrder &&
                this.Picked == other.Picked &&
                this.Description == other.Description &&
                this.Keycount == other.Keycount &&
                this.Price == other.Price &&
                this.Ownership == other.Ownership &&
                this.Guttable == other.Guttable &&
                (this.Type == other.Type || this.Type.Equals(other.Type)) &&
                (this.Brand == other.Brand || this.Brand.Equals(other.Brand)) &&
                (this.Core == other.Core || this.Core.Equals(other.Core));
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            foreach (PropertyInfo p in this.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty).Where(x => x.CustomAttributes.Any(y => y.AttributeType == typeof(RequiredAttribute))))
            {
                Validator.TryValidateProperty(p.GetValue(this), new ValidationContext(this) { MemberName = p.Name }, results);
            }

            return results;
        }
    }

    public class CollectionLocksComparer : IEqualityComparer<CollectionLocks>
    {
        public bool Equals(CollectionLocks x, CollectionLocks y)
        {
            return x.DatabaseId == y.DatabaseId &&
                x.Name == y.Name &&
                x.Model == y.Model &&
                x.Modelname == y.Modelname &&
                x.BindingOrder == y.BindingOrder &&
                x.Picked == y.Picked &&
                x.Description == y.Description &&
                x.Keycount == y.Keycount &&
                x.Price == y.Price &&
                x.Ownership == y.Ownership &&
                x.Guttable == y.Guttable &&
                (x.Type == y.Type || x.Type.Equals(y.Type)) &&
                (x.Brand == y.Brand || x.Brand.Equals(y.Brand)) &&
                (x.Core == y.Core || x.Core.Equals(y.Core));
        }

        public int GetHashCode([DisallowNull] CollectionLocks obj)
        {
            return (obj.DatabaseId == null ? 0 : obj.DatabaseId.GetHashCode()) ^
                (obj.Name == null ? 0 : obj.Name.GetHashCode()) ^
                (obj.Model == null ? 0 : obj.Model.GetHashCode()) ^
                (obj.Modelname == null ? 0 : obj.Modelname.GetHashCode()) ^
                (obj.BindingOrder == null ? 0 : obj.BindingOrder.GetHashCode()) ^
                obj.Picked.GetHashCode() ^
                (obj.Description == null ? 0 : obj.Description.GetHashCode()) ^
                obj.Keycount.GetHashCode() ^
                obj.Price.GetHashCode() ^
                obj.Ownership.GetHashCode() ^
                obj.Guttable.GetHashCode();
        }
    }
}
