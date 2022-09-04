
namespace LockpickersGuide.Models
{
    public class Brand : IModelItem
    {
        public int DatabaseId { get; set; }
        public string Name { get; set; }
        public Country Country { get; set; }
        public int Established { get; set; }
    }
}
