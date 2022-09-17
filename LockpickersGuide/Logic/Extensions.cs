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
