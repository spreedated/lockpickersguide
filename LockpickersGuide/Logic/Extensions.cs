using LockpickersGuide.PresentiationModels;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace LockpickersGuide.Logic
{
    internal static class Extensions
    {
        public static bool IsSet(this object s)
        {
            return s != null;
        }

        public static bool IsSet(this string s)
        {
            return s != null && s.Length > 0;
        }

        public static bool IsSet(this int s)
        {
            return !s.Equals(default);
        }
        public static void Switch(this Frame frame, IEnumerable<LockpickerPage> pages, string pagename)
        {
            frame.Navigate(pages.FirstOrDefault(x => x.Pagename.ToLower() == pagename.ToLower() || x.Pagename.ToLower().StartsWith(pagename.ToLower())));
        }
    }
}
