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
        public static Brand GetBrand(int id)
        {
            if (ObjectStorage.Brands.Any(x => x.DatabaseId == id))
            {
                Log.Debug($"[Database][GetBrand] ObjectStorage hit \"{id}\"");
                return ObjectStorage.Brands.First(x => x.DatabaseId == id);
            }

            Log.Debug($"[Database][GetBrand] ObjectStorage miss \"{id}\"");

            using (NpgsqlConnection conn = new(Options.Instance.DatabaseCredentials.ToString()))
            {
                conn.Open();

                using (NpgsqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"SELECT * FROM {DB_DATABASE_LOCKPICKING}.{DB_SCHEMA_ATTRIBUTES}.{DB_TABLE_BRANDS} WHERE id = {id} LIMIT 1;";

                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        dr.Read();

                        Brand c = new()
                        {
                            DatabaseId = dr.GetInt32(0),
                            Name = dr.GetString(1),
                            Country = GetCountry(dr.GetInt32(2)),
                            Founded = dr.IsDBNull(3) ? null : dr.GetInt32(3),
                            City = dr.IsDBNull(4) ? null : dr.GetString(4),
                            Website = dr.IsDBNull(5) ? null : dr.GetString(5),
                            AltName = dr.IsDBNull(6) ? null : dr.GetString(6),
                            Description = dr.IsDBNull(7) ? null : dr.GetString(7)
                        };

                        ObjectStorage.Brands.Add(c);

                        return ObjectStorage.Brands.First(x => x.DatabaseId == id);
                    }
                }
            }
        }

        public static IEnumerable<Brand> GetBrands()
        {
            using (NpgsqlConnection conn = new(Options.Instance.DatabaseCredentials.ToString()))
            {
                conn.Open();

                using (NpgsqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"SELECT * FROM {DB_DATABASE_LOCKPICKING}.{DB_SCHEMA_ATTRIBUTES}.{DB_TABLE_BRANDS} ORDER BY name ASC;";

                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Brand b = new()
                            {
                                DatabaseId = dr.GetInt32(0),
                                Name = dr.GetString(1),
                                Country = GetCountry(dr.GetInt32(2)),
                                Founded = dr.IsDBNull(3) ? null : dr.GetInt32(3),
                                City = dr.IsDBNull(4) ? null : dr.GetString(4),
                                Website = dr.IsDBNull(5) ? null : dr.GetString(5),
                                AltName = dr.IsDBNull(6) ? null : dr.GetString(6),
                                Description = dr.IsDBNull(7) ? null : dr.GetString(7)
                            };

                            ObjectStorage.Brands.Add(b);

                            yield return b;
                        }
                    }
                }

                conn.Close();
            }
        }
    }
}
