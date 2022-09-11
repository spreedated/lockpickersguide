using LockpickersGuide.Datastructure;
using LockpickersGuide.EventArgs;
using LockpickersGuide.Models;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static LockpickersGuide.Logic.Constants;
using static LockpickersGuide.Logic.Database;

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

            FillObjectStorage<Country>(ref ObjectStorage.Countries, () => GetCountries(), CACHE_COUNTRIES);
            PreloadStep?.Invoke(null, System.EventArgs.Empty);

            FillObjectStorage<Brand>(ref ObjectStorage.Brands, () => GetBrands(), CACHE_BRANDS);
            PreloadStep?.Invoke(null, System.EventArgs.Empty);

            FillObjectStorage<Core>(ref ObjectStorage.Cores, () => GetCores(), CACHE_CORES);
            PreloadStep?.Invoke(null, System.EventArgs.Empty);

            FillObjectStorage<Locktype>(ref ObjectStorage.Locktypes, () => GetLocktypes(), CACHE_LOCKTYPES);
            PreloadStep?.Invoke(null, System.EventArgs.Empty);

            s.Stop();

            PreloadComplete?.Invoke(null, new(s.Elapsed));
            return true;
        }

        private static void PreloadCompleted(object sender, PreloadCompleteEventArgs e)
        {
            Log.Information($"[Preload] Completed in \"{e.Duration:mm\\:ss\\:ffffff}\"");
        }

        private static void FillObjectStorage<T>(ref HashSetLockpicker<T> hs, Func<IEnumerable<T>> p, string cachekey) where T : IModelItem
        {
            if (Cache.IsAvailable)
            {
                var cache = RedisConnectorHelper.Connection.GetDatabase();
                string json = cache.StringGet(cachekey).ToString();

                if (json != null && json.Length > 0)
                {
                    Log.Debug($"[Preload][FillObjectStorage<{typeof(T).Name}>] Cache hit");
                    hs = JsonConvert.DeserializeObject<HashSetLockpicker<T>>(json);

                    return;
                }
            }

            Log.Debug($"[Preload][FillObjectStorage<{typeof(T).Name}>] Cache miss");

            foreach (T c in p().OrderBy(x => x.Name))
            {
                hs.Add(c);
            }

            string jsonn = JsonConvert.SerializeObject(hs, Formatting.Indented);
            if (RedisConnectorHelper.Connection.GetDatabase().StringSet(cachekey, jsonn, expiry: new TimeSpan(12, 0, 0)))
            {
                Log.Debug($"[Preload][FillObjectStorage<{typeof(T).Name}>] Cache warmed successfully");
            }
            else
            {
                Log.Warning($"[Preload][FillObjectStorage<{typeof(T).Name}>] Cache warming failure");
            }
        }
    }
}
