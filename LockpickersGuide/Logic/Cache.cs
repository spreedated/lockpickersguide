using StackExchange.Redis;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Serilog;
using System.Linq;
using System.Reflection;

namespace LockpickersGuide.Logic
{
    internal static class Cache
    {
        public static bool IsAvailable
        {
            get
            {
                return IsOptionAvailable();
            }
        }

        private static bool IsOptionAvailable()
        {
            TimeSpan? t = RedisConnectorHelper.Connection?.GetDatabase().Ping();

            if (t == null)
            {
                Log.Debug($"[Cache][ClearEntireCache] No connection to caching server");
                return false;
            }

            Log.Debug($"[Cache][IsAvailable] Redis available - ping {t?.TotalMilliseconds}ms // {t:ss\\:ffffff}");

            return true;
        }

        public static bool UpdateCache<T>(string key, IEnumerable<T> itemDatastructure)
        {
            IDatabase cache = RedisConnectorHelper.Connection.GetDatabase();
            Log.Debug($"[UpdateCache<{typeof(T).Name}>] Cache warming");
            return cache.StringSet(key, JsonConvert.SerializeObject(itemDatastructure), expiry: new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 1, 0, 0, 0) - DateTime.Now);
        }

        public static bool ClearEntireCache()
        {
            IEnumerable<KeyValuePair<string, string>> kkv = typeof(Constants).GetFields(BindingFlags.Public | BindingFlags.Static).Where(x => x.Name.ToLower().StartsWith("cache")).Select(x => new KeyValuePair<string, string>(x.Name,x.GetValue(x).ToString()));

            IDatabase cache = RedisConnectorHelper.Connection?.GetDatabase();

            if (cache == null)
            {
                Log.Debug($"[Cache][ClearEntireCache] No connection to caching server");
                return false;
            }

            long keycount = cache.KeyDelete(kkv.Select(x=> new RedisKey(x.Value)).ToArray());

            Log.Debug($"[Cache][ClearEntireCache] Deleted {keycount}/{kkv.Count()}");
            Log.Verbose($"[Cache][ClearEntireCache] Deleted {string.Join(" - ", kkv.Select(x=> $"Key: \"{x.Key}\" -> \"{x.Value}\"" )).TrimEnd().TrimEnd('-').TrimEnd()}");

            return kkv.Count() == keycount;
        }
    }
}
