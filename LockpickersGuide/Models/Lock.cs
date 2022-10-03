namespace LockpickersGuide.Models
{
    public sealed class Lock
    {
        public int DatabaseId { get; set; }
        public Locktype Type { get; set; }
        public Brand Brand { get; set; }
        public string Modelname { get; set; }
        public string Model { get; set; }
        public Core Core { get; set; }
        public string Description { get; set; }
        public int? Keycount { get; set; }
        public bool? Gutable { get; set; }
        public string Link { get; set; }
        public string Imagelink { get; set; }
        public Color Color { get; set; }
    }
}
