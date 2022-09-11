using LockpickersGuide.Datastructure;
using LockpickersGuide.Models;
using StackExchange.Redis;
using System;

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

        public static void GetCache()
        {
            
        }

        public static void ReadData()
        {
            var cache = RedisConnectorHelper.Connection.GetDatabase();
            var devicesCount = 10000;
            for (int i = 0; i < devicesCount; i++)
            {
                var value = cache.StringGet($"Device_Status:{i}");
                Console.WriteLine($"Valor={value}");
            }
        }

        public static void SaveBigData()
        {
            var devicesCount = 10000;
            var rnd = new Random();
            var cache = RedisConnectorHelper.Connection.GetDatabase();

            for (int i = 1; i < devicesCount; i++)
            {
                var value = rnd.Next(0, 10000);
                cache.StringSet($"Device_Status:{i}", value);
            }
        }
    }
}
