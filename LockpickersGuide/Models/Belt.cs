namespace LockpickersGuide.Models
{
    public class Belt : IModelItem
    {
        public int DatabaseId { get; set; }
        public string Name { get; set; }
        public string Imagelink { get; set; }
        public string Description { get; set; }
    }
}
