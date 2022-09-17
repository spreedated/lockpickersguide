#pragma warning disable S3963

using Serilog;
using StackExchange.Redis;
using System;

namespace LockpickersGuide.Logic
{
    internal static class RedisConnectorHelper
    {
        static RedisConnectorHelper()
        {
            RedisConnectorHelper.lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
            {
                try
                {
                    return ConnectionMultiplexer.Connect(Options.Instance.RedisConnection.ToString());
                }
                catch (Exception ex)
                {
                    Log.Error(ex, $"[Cache][IsAvailable] Redis unavailable");
                    return null;
                }
            });
        }

        private static Lazy<ConnectionMultiplexer> lazyConnection;

        public static ConnectionMultiplexer Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }
    }
}
