using StackExchange.Redis;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Serilog;

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
            return Options.Instance.RedisConnection != null;
        }

        public static bool UpdateCache<T>(string key, IEnumerable<T> itemDatastructure)
        {
            IDatabase cache = RedisConnectorHelper.Connection.GetDatabase();
            Log.Debug($"[UpdateCache<{typeof(T).Name}>] Cache warming");
            return cache.StringSet(key, JsonConvert.SerializeObject(itemDatastructure), expiry: new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 1, 0, 0, 0) - DateTime.Now);
        }
    }
}
