namespace LockpickersGuide.Models
{
    internal interface IModelItem
    {
        int DatabaseId { get; set; }
        string Name { get; set; }
    }
}
