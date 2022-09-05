using System.IO;
using System.Reflection;

namespace UnitTests
{
    internal static class Variables
    {
        internal static readonly string testFilePath = Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..\\..\\..\\..\\..", "UnitTestFiles"));
    }
}
