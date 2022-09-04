using LockpickersGuide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static LockpickersGuide.Logic.Database;

namespace LockpickersGuide.Logic
{
    internal static class Preload
    {
        public static event EventHandler PreloadStep;
        public static event EventHandler PreloadComplete;

        public static bool Load()
        {
            Options.Initialize();
            PreloadStep?.Invoke(null, EventArgs.Empty);

            if (!AreCredentialsValid())
            {
                return false;
            }
            PreloadStep?.Invoke(null, EventArgs.Empty);

            LoadCountries();
            PreloadStep?.Invoke(null, EventArgs.Empty);

            LoadBrands();
            PreloadStep?.Invoke(null, EventArgs.Empty);

            LoadCores();
            PreloadStep?.Invoke(null, EventArgs.Empty);

            LoadLocktypes();
            PreloadStep?.Invoke(null, EventArgs.Empty);

            PreloadComplete?.Invoke(null, EventArgs.Empty);
            return true;
        }

        private static void LoadBrands()
        {
            foreach (Brand brand in GetBrands().OrderBy(x => x.Name))
            {
                Cache.Brands.Add(brand);
            }
        }

        private static void LoadCores()
        {
            foreach (Core c in GetCores().OrderBy(x => x.Name))
            {
                Cache.Cores.Add(c);
            }
        }

        private static void LoadLocktypes()
        {
            foreach (Locktype l in GetLocktypes().OrderBy(x => x.Name))
            {
                Cache.Locktypes.Add(l);
            }
        }

        private static void LoadCountries()
        {
            foreach (Country c in GetCountries().OrderBy(x => x.Name))
            {
                Cache.Countries.Add(c);
            }
        }
    }
}
