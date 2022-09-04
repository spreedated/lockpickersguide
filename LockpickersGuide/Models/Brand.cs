
using System;

namespace LockpickersGuide.Models
{
    public class Brand : IModelItem
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
    }
}
