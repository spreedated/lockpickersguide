using LockpickersGuide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using static LockpickersGuide.Logic.Constants;

namespace LockpickersGuide.Logic
{
    internal static class Database
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
                            yield return new Brand()
                            {
                                DatabaseId = dr.GetInt32(0),
                                Name = dr.GetString(1),
                                Country = GetCountry(dr.GetInt32(2)),
                                Established = dr.GetInt32(3),
                            };
                        }
                    }
                }

                conn.Close();
            }
        }

        public static Country GetCountry(int id)
        {
            using (NpgsqlConnection conn = new(Options.Instance.DatabaseCredentials.ToString()))
            {
                conn.Open();

                using (NpgsqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"SELECT * FROM {DB_DATABASE_LOCKPICKING}.{DB_SCHEMA_ATTRIBUTES}.{DB_TABLE_COUNTRIES} WHERE id = {id} LIMIT 1;";

                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        dr.Read();

                        return new Country()
                        {
                            DatabaseId = dr.GetInt32(0),
                            Iso = dr.GetString(1),
                            Iso3 = dr.GetString(4),
                            Name = dr.GetString(2),
                            Nicename = dr.GetString(3)
                        };
                    }
                }

                conn.Close();
            }
        }
    }
}
