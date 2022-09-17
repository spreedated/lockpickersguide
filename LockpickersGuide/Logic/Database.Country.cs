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
        public static IEnumerable<Country> GetCountries()
        {
            using (NpgsqlConnection conn = new(Options.Instance.DatabaseCredentials.ToString()))
            {
                conn.Open();

                using (NpgsqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"SELECT * FROM {DB_DATABASE_LOCKPICKING}.{DB_SCHEMA_ATTRIBUTES}.{DB_TABLE_COUNTRIES};";

                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Country c = new()
                            {
                                DatabaseId = dr.GetInt32(0),
                                Iso = dr.GetString(1),
                                Iso3 = dr.IsDBNull(4) ? null : dr.GetString(4),
                                Name = dr.GetString(2),
                                Nicename = dr.GetString(3)
                            };

                            ObjectStorage.Countries.Add(c);

                            yield return c;
                        }
                    }
                }

                conn.Close();
            }
        }

        public static Country GetCountry(int id)
        {
            if (ObjectStorage.Countries.Any(x => x.DatabaseId == id))
            {
                Log.Debug($"[Database][GetCountry] ObjectStorage hit \"{id}\"");
                return ObjectStorage.Countries.First(x => x.DatabaseId == id);
            }

            Log.Debug($"[Database][GetCountry] ObjectStorage miss \"{id}\"");

            using (NpgsqlConnection conn = new(Options.Instance.DatabaseCredentials.ToString()))
            {
                conn.Open();

                using (NpgsqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"SELECT * FROM {DB_DATABASE_LOCKPICKING}.{DB_SCHEMA_ATTRIBUTES}.{DB_TABLE_COUNTRIES} WHERE id = {id} LIMIT 1;";

                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        dr.Read();

                        Country c = new()
                        {
                            DatabaseId = dr.GetInt32(0),
                            Iso = dr.GetString(1),
                            Iso3 = dr.GetString(4),
                            Name = dr.GetString(2),
                            Nicename = dr.GetString(3)
                        };

                        ObjectStorage.Countries.Add(c);

                        return ObjectStorage.Countries.First(x => x.DatabaseId == id);
                    }
                }
            }
        }
    }
}
