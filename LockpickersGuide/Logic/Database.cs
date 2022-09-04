using LockpickersGuide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using static LockpickersGuide.Logic.Constants;
using Serilog;

namespace LockpickersGuide.Logic
{
    internal static partial class Database
    {
        public static IEnumerable<Brand> GetBrands()
        {
            using (NpgsqlConnection conn = new(Options.Instance.DatabaseCredentials.ToString()))
            {
                conn.Open();

                using (NpgsqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"SELECT * FROM {DB_DATABASE_LOCKPICKING}.{DB_SCHEMA_ATTRIBUTES}.{DB_TABLE_BRANDS};";

                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Brand b = new()
                            {
                                DatabaseId = dr.GetInt32(0),
                                Name = dr.GetString(1),
                                Country = GetCountry(dr.GetInt32(2)),
                                Established = dr.GetInt32(3),
                            };

                            Cache.Brands.Add(b);

                            yield return b;
                        }
                    }
                }

                conn.Close();
            }
        }

        public static Country GetCountry(int id)
        {
            if (Cache.Countries.Any(x => x.DatabaseId == id))
            {
                Log.Information($"[Database][GetCountry] Cache hit \"{id}\"");
                return Cache.Countries.First(x => x.DatabaseId == id);
            }

            Log.Information($"[Database][GetCountry] Cache miss \"{id}\"");

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

                        Cache.Countries.Add(c);

                        return Cache.Countries.First(x => x.DatabaseId == id);
                    }
                }
            }
        }
    }
}
