using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LockpickersGuide.Logic
{
    internal static class Variables
    {
        internal static string OptionsFilepath { get; set; } = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "options.json");
    }
}
