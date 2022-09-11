using StackExchange.Redis;
using System.Collections.Generic;

namespace LockpickersGuide.Models
{
    public class RedisConnection
    {
        /// <summary>
        /// Object identifier
        /// </summary>
        public string Name { get; set; }
        public string Password { get; set; }
        /// <summary>
        /// Only using Host/Server and Port
        /// </summary>
        public IEnumerable<Endpoint> Endpoints { get; set; }

        public override string ToString()
        {
            ConfigurationOptions o = new();

            foreach (Endpoint e in this.Endpoints)
            {
                o.EndPoints.Add($"{e.Server}:{e.Port}");
            }

            o.Password = this.Password;

            return o.ToString();
        }

        public class Endpoint
        {
            public string Server { get; set; }
            public int Port { get; set; }
        }
    }
}
