using LockpickersGuide.Models;
using Npgsql;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LockpickersGuide.Logic.Constants;

namespace LockpickersGuide.Logic
{
    internal partial class Database
    {
        public static Core GetCore(int id)
        {
            if (ObjectStorage.Cores.Any(x => x.DatabaseId == id))
            {
                Log.Debug($"[Database][GetCore] ObjectStorage hit \"{id}\"");
                return ObjectStorage.Cores.First(x => x.DatabaseId == id);
            }

            Log.Debug($"[Database][GetCore] ObjectStorage miss \"{id}\"");

            using (NpgsqlConnection conn = new(Options.Instance.DatabaseCredentials.ToString()))
            {
                conn.Open();

                using (NpgsqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"SELECT * FROM {DB_DATABASE_LOCKPICKING}.{DB_SCHEMA_ATTRIBUTES}.{DB_TABLE_BRANDS} WHERE id = {id} LIMIT 1;";

                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        dr.Read();

                        Core c = new()
                        {
                            DatabaseId = dr.GetInt32(0),
                            Name = dr.GetString(1)
                        };

                        ObjectStorage.Cores.Add(c);

                        return ObjectStorage.Cores.First(x => x.DatabaseId == id);
                    }
                }
            }
        }

        public static IEnumerable<Core> GetCores()
        {
            using (NpgsqlConnection conn = new(Options.Instance.DatabaseCredentials.ToString()))
            {
                conn.Open();

                using (NpgsqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"SELECT * FROM {DB_DATABASE_LOCKPICKING}.{DB_SCHEMA_ATTRIBUTES}.{DB_TABLE_CORES};";

                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Core c = new()
                            {
                                DatabaseId = dr.GetInt32(0),
                                Name = dr.GetString(1)
                            };

                            ObjectStorage.Cores.Add(c);

                            yield return c;
                        }
                    }
                }

                conn.Close();
            }
        }
    }
}
