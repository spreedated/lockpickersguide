using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using Newtonsoft.Json;

namespace LockpickersGuide.Models
{
    internal class Options
    {
        [JsonProperty("databasecredentials")]
        public NpgsqlConnectionStringBuilder DatabaseCredentials { get; set; } = new() { Host = "", Port = 5432, Database = "", Username = "", Password = "" };
    }
}
