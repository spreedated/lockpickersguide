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
        public static IEnumerable<CollectionLocks> GetCollectionLocks()
        {
            using (NpgsqlConnection conn = new(Options.Instance.DatabaseCredentials.ToString()))
            {
                conn.Open();

                using (NpgsqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"SELECT * FROM {DB_DATABASE_LOCKPICKING}.{DB_SCHEMA_COLLECTION}.{DB_TABLE_LOCKS} ORDER BY id ASC;";

                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            CollectionLocks l = new()
                            {
                                DatabaseId = dr.GetString(0),
                                Type = GetLocktype(dr.GetInt32(1)),
                                Brand = GetBrand(dr.GetInt32(2)),
                                Modelname = dr.IsDBNull(3) ? null : dr.GetString(3),
                                Model = dr.IsDBNull(4) ? null : dr.GetString(4),
                                BindingOrder = dr.IsDBNull(5) ? null : dr.GetString(5),
                                Picked = dr.GetBoolean(6),
                                Core = GetCore(dr.GetInt32(7)),
                                Description = dr.IsDBNull(8) ? null : dr.GetString(8),
                                Keycount = dr.GetInt32(9),
                                Price = dr.GetInt32(10),
                                Ownership = dr.GetBoolean(11),
                                Guttable = dr.GetBoolean(12)
                            };

                            //Cache..Add(l);

                            yield return l;
                        }
                    }
                }

                conn.Close();
            }
        }

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
