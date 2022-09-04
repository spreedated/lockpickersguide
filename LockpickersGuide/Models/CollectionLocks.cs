using System;

namespace LockpickersGuide.Models
{
    public class CollectionLocks : ICollectionLock
    {
        public string DatabaseId { get; set; }
        public string Name
        {
            get
            {
                return this.Model;
            }
        }

        public Locktype Type { get; set; }
        public Brand Brand { get; set; }
        public string Modelname { get; set; }
        public string Model { get; set; }
        public string BindingOrder { get; set; }
        public bool Picked { get; set; }
        public Core Core { get; set; }
        public string Description { get; set; }
        public int Keycount { get; set; }
        public double Price { get; set; }
        public bool Ownership { get; set; }
        public bool Guttable { get; set; }
    }
}
