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
        public const int Steps = 10;
        public static event EventHandler PreloadStep;
        public static event EventHandler<PreloadCompleteEventArgs> PreloadComplete;

        public static bool Load()
        {
            PreloadComplete += PreloadCompleted;

            Stopwatch s = new();
            s.Start();

            Options.Initialize();
            PreloadStep?.Invoke(null, System.EventArgs.Empty);
            //Options.Instance.ForceDatabaseReload = true;
            if (!AreDatabaseCredentialsValid())
            {
                return false;
            }

            PreloadStep?.Invoke(null, System.EventArgs.Empty);

            CheckRedisAvailablity();
            PreloadStep?.Invoke(null, System.EventArgs.Empty);

            FillObjectStorage<Country>(ref ObjectStorage.Countries, () => GetCountries(), CACHE_COUNTRIES);
            PreloadStep?.Invoke(null, System.EventArgs.Empty);

            FillObjectStorage<Brand>(ref ObjectStorage.Brands, () => GetBrands(), CACHE_BRANDS);
            PreloadStep?.Invoke(null, System.EventArgs.Empty);

            FillObjectStorage<Core>(ref ObjectStorage.Cores, () => GetCores(), CACHE_CORES);
            PreloadStep?.Invoke(null, System.EventArgs.Empty);

            FillObjectStorage<Locktype>(ref ObjectStorage.Locktypes, () => GetLocktypes(), CACHE_LOCKTYPES);
            PreloadStep?.Invoke(null, System.EventArgs.Empty);

            FillObjectStorage<Belt>(ref ObjectStorage.Belts, () => GetBelts(), CACHE_BELTS);
            PreloadStep?.Invoke(null, System.EventArgs.Empty);

            FillObjectStorage<Color>(ref ObjectStorage.Colors, () => GetColors(), CACHE_COLORS);
            PreloadStep?.Invoke(null, System.EventArgs.Empty);

            FillObjectStorage<Bodywidth>(ref ObjectStorage.Bodywidths, () => GetBodywidths(), CACHE_BODYWIDTHS);
            PreloadStep?.Invoke(null, System.EventArgs.Empty);

            s.Stop();

            PreloadComplete?.Invoke(null, new(s.Elapsed));
            return true;
        }

        private static void PreloadCompleted(object sender, PreloadCompleteEventArgs e)
        {
            Log.Information($"[Preload] Completed in \"{e.Duration:mm\\:ss\\:ffffff}\"");
        }

        private static void CheckRedisAvailablity()
        {
            Variables.IsRedisAvailable = Cache.IsAvailable;
        }

        public static void FillObjectStorage<T>(ref HashSetLockpicker<T> hs, Func<IEnumerable<T>> p, string cachekey) where T : IModelItem
        {
            hs.Clear();
            if (Variables.IsRedisAvailable && !Options.Instance.ForceDatabaseReload)
            {
                var cache = RedisConnectorHelper.Connection.GetDatabase();
                string json = cache.StringGet(cachekey).ToString();

                if (json != null && json.Length > 0)
                {
                    Log.Debug($"[Preload][FillObjectStorage<{typeof(T).Name}>] Redis Cache hit");
                    hs = JsonConvert.DeserializeObject<HashSetLockpicker<T>>(json);

                    return;
                }
            }

            if (!Options.Instance.ForceDatabaseReload)
            {
                Log.Debug($"[Preload][FillObjectStorage<{typeof(T).Name}>] Redis Cache miss");
            }else
            {
                Log.Debug($"[Preload][FillObjectStorage<{typeof(T).Name}>] Force reload");
            }

            foreach (T c in p().OrderBy(x => x.Name))
            {
                hs.Add(c);
            }

            if (Variables.IsRedisAvailable)
            {
                string jsonn = JsonConvert.SerializeObject(hs, Formatting.Indented);
                if (RedisConnectorHelper.Connection.GetDatabase().StringSet(cachekey, jsonn, expiry: new TimeSpan(12, 0, 0)))
                {
                    Log.Debug($"[Preload][FillObjectStorage<{typeof(T).Name}>] Runtime Cache warmed successfully");
                    Cache.UpdateCache<T>(cachekey, hs);
                }
                else
                {
                    Log.Warning($"[Preload][FillObjectStorage<{typeof(T).Name}>] Runtime Cache warming failure");
                }
            }
        }
    }
}
