using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LockpickersGuide.Logic
{
    internal static class Extensions
    {
        public static bool IsSet(this string s)
        {
            return s != null && s.Length > 0;
        }

        public static bool IsSet(this int s)
        {
            return !s.Equals(default);
        }
    }
}
