using LockpickersGuide.Models;
using Npgsql;
using System.Collections.Generic;
using static LockpickersGuide.Logic.Constants;

namespace LockpickersGuide.Logic
{
    internal partial class Database
    {
        public static IEnumerable<Bodywidth> GetBodywidths()
        {
            using (NpgsqlConnection conn = new(Options.Instance.DatabaseCredentials.ToString()))
            {
                conn.Open();

                using (NpgsqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"SELECT * FROM {DB_DATABASE_LOCKPICKING}.{DB_SCHEMA_ATTRIBUTES}.{DB_TABLE_BODYWIDTHS};";

                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Bodywidth b = new()
                            {
                                DatabaseId = dr.GetInt32(0),
                                Inch = dr.GetString(1),
                                Mm = dr.GetDouble(2)
                            };

                            ObjectStorage.Bodywidths.Add(b);

                            yield return b;
                        }
                    }
                }

                conn.Close();
            }
        }
    }
}
