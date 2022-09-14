using Newtonsoft.Json;
using Npgsql;

namespace LockpickersGuide.Models
{
    internal class Options
    {
        [JsonProperty("databasecredentials")]
        public NpgsqlConnectionStringBuilder DatabaseCredentials { get; set; } = new() { Host = "", Port = 5432, Database = "", Username = "", Password = "" };
        public RedisConnection RedisConnection { get; set; }
    }
}
