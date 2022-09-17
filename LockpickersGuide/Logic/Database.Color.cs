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
        public static IEnumerable<Color> GetColors()
        {
            using (NpgsqlConnection conn = new(Options.Instance.DatabaseCredentials.ToString()))
            {
                conn.Open();

                using (NpgsqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"SELECT * FROM {DB_DATABASE_LOCKPICKING}.{DB_SCHEMA_ATTRIBUTES}.{DB_TABLE_COLORS};";

                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Color b = new()
                            {
                                DatabaseId = dr.GetInt32(0),
                                Name = dr.GetString(1)
                            };

                            ObjectStorage.Colors.Add(b);

                            yield return b;
                        }
                    }
                }

                conn.Close();
            }
        }
    }
}
