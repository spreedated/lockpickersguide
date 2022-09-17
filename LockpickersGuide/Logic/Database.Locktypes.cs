using LockpickersGuide.Models;
using Npgsql;
using Serilog;
using System.Collections.Generic;
using System.Linq;
using static LockpickersGuide.Logic.Constants;

namespace LockpickersGuide.Logic
{
    internal partial class Database
    {
        public static Locktype GetLocktype(int id)
        {
            if (ObjectStorage.Locktypes.Any(x => x.DatabaseId == id))
            {
                Log.Debug($"[Database][GetLocktype] ObjectStorage hit \"{id}\"");
                return ObjectStorage.Locktypes.First(x => x.DatabaseId == id);
            }

            Log.Debug($"[Database][GetLocktype] ObjectStorage miss \"{id}\"");

            using (NpgsqlConnection conn = new(Options.Instance.DatabaseCredentials.ToString()))
            {
                conn.Open();

                using (NpgsqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"SELECT * FROM {DB_DATABASE_LOCKPICKING}.{DB_SCHEMA_ATTRIBUTES}.{DB_TABLE_LOCKTYPES} WHERE id = {id} LIMIT 1;";

                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        dr.Read();

                        Locktype c = new()
                        {
                            DatabaseId = dr.GetInt32(0),
                            Name = dr.GetString(1)
                        };

                        ObjectStorage.Locktypes.Add(c);

                        return ObjectStorage.Locktypes.First(x => x.DatabaseId == id);
                    }
                }
            }
        }

        public static IEnumerable<Locktype> GetLocktypes()
        {
            using (NpgsqlConnection conn = new(Options.Instance.DatabaseCredentials.ToString()))
            {
                conn.Open();

                using (NpgsqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"SELECT * FROM {DB_DATABASE_LOCKPICKING}.{DB_SCHEMA_ATTRIBUTES}.{DB_TABLE_LOCKTYPES};";

                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Locktype l = new()
                            {
                                DatabaseId = dr.GetInt32(0),
                                Name = dr.GetString(1)
                            };

                            ObjectStorage.Locktypes.Add(l);

                            yield return l;
                        }
                    }
                }

                conn.Close();
            }
        }
    }
}
