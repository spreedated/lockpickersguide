using LockpickersGuide.Datastructure;
using LockpickersGuide.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static LockpickersGuide.Logic.Database;
using static LockpickersGuide.Logic.Constants;
using System.Configuration;
using Serilog;
using LockpickersGuide.EventArgs;
using System.Diagnostics;

namespace LockpickersGuide.Logic
{
    internal static class Preload
    {
        public static event EventHandler PreloadStep;
        public static event EventHandler<PreloadCompleteEventArgs> PreloadComplete;

        public static bool Load()
        {
            PreloadComplete += PreloadCompleted;

            Stopwatch s = new();
            s.Start();

            Options.Initialize();
            PreloadStep?.Invoke(null, System.EventArgs.Empty);

            if (!AreCredentialsValid())
            {
                return false;
            }
            PreloadStep?.Invoke(null, System.EventArgs.Empty);

            LoadCountries();
            PreloadStep?.Invoke(null, System.EventArgs.Empty);

            LoadBrands();
            PreloadStep?.Invoke(null, System.EventArgs.Empty);

            LoadCores();
            PreloadStep?.Invoke(null, System.EventArgs.Empty);

            LoadLocktypes();
            PreloadStep?.Invoke(null, System.EventArgs.Empty);

            s.Stop();

            PreloadComplete?.Invoke(null, new(s.Elapsed));
            return true;
        }

        private static void PreloadCompleted(object sender, PreloadCompleteEventArgs e)
        {
            Log.Information($"[Preload] Completed in \"{e.Duration:mm\\:ss\\:ffffff}\"");
        }

        private static void LoadBrands()
        {
            foreach (Brand brand in GetBrands().OrderBy(x => x.Name))
            {
                ObjectStorage.Brands.Add(brand);
            }
        }

        private static void LoadCores()
        {
            foreach (Core c in GetCores().OrderBy(x => x.Name))
            {
                ObjectStorage.Cores.Add(c);
            }
        }

        private static void LoadLocktypes()
        {
            foreach (Locktype l in GetLocktypes().OrderBy(x => x.Name))
            {
                ObjectStorage.Locktypes.Add(l);
            }
        }

        private static void LoadCountries()
        {
            if (Cache.IsAvailable)
            {
                var cache = RedisConnectorHelper.Connection.GetDatabase();
                string json = cache.StringGet(CACHE_COUNTRIES).ToString();

                if (json != null && json.Length > 0)
                {
                    Log.Debug("[Preload][LoadCountries] Cache hit");
                    ObjectStorage.Countries = JsonConvert.DeserializeObject<HashSetLockpicker<Country>>(json);

                    return;
                }
            }

            Log.Debug("[Preload][LoadCountries] Cache miss");

            foreach (Country c in GetCountries().OrderBy(x => x.Name))
            {
                ObjectStorage.Countries.Add(c);
            }

            string jsonn = JsonConvert.SerializeObject(ObjectStorage.Countries, Formatting.Indented);
            if (RedisConnectorHelper.Connection.GetDatabase().StringSet(CACHE_COUNTRIES, jsonn, expiry: new TimeSpan(12,0,0)))
            {
                Log.Debug("[Preload][LoadCountries] Cache warmed successfully");
            }
            else
            {
                Log.Warning("[Preload][LoadCountries] Cache warming failure");
            }
        }
    }
}
