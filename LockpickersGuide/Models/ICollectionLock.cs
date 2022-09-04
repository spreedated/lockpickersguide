namespace LockpickersGuide.Models
{
    internal interface ICollectionLock
    {
        string DatabaseId { get; set; }
        string Name { get; }
    }
}
