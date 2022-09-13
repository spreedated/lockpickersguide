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
        public static IEnumerable<Belt> GetBelts()
        {
            using (NpgsqlConnection conn = new(Options.Instance.DatabaseCredentials.ToString()))
            {
                conn.Open();

                using (NpgsqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"SELECT * FROM {DB_DATABASE_LOCKPICKING}.{DB_SCHEMA_ATTRIBUTES}.{DB_TABLE_BELTS};";

                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Belt b = new()
                            {
                                DatabaseId = dr.GetInt32(0),
                                Name = dr.GetString(1),
                                Imagelink = dr.GetString(2),
                                Description = dr.GetString(3)
                            };

                            ObjectStorage.Belts.Add(b);

                            yield return b;
                        }
                    }
                }

                conn.Close();
            }
        }
    }
}
