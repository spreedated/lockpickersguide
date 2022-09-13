using System.IO;
using System.Reflection;

namespace LockpickersGuide.Logic
{
    internal static class Variables
    {
        internal static string OptionsFilepath { get; set; } = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "options.json");
        internal static bool IsRedisAvailable { get; set; }
    }
}
