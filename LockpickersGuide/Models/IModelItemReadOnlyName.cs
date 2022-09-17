namespace LockpickersGuide.Models
{
    internal interface IModelItemReadOnlyName
    {
        int DatabaseId { get; set; }
        string Name { get; }
    }
}
