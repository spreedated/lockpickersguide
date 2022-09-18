using LockpickersGuide.Models;
using Npgsql;
using System.Collections.Generic;
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

        public static bool AddBelt(Belt belt)
        {
            using (NpgsqlConnection conn = new(Options.Instance.DatabaseCredentials.ToString()))
            {
                conn.Open();

                using (NpgsqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"INSERT INTO {DB_DATABASE_LOCKPICKING}.{DB_SCHEMA_ATTRIBUTES}.{DB_TABLE_BELTS} (name,imagelink,description) VALUES (@name,@imagelink,@desc) RETURNING id;";
                    cmd.Parameters.AddWithValue("@name", belt.Name);
                    cmd.Parameters.AddWithValue("@imagelink", belt.Imagelink);
                    cmd.Parameters.AddWithValue("@desc", belt.Description);

                    return cmd.ExecuteNonQuery() == 1;
                }
            }
        }

        public static bool UpdateBelt(Belt belt)
        {
            using (NpgsqlConnection conn = new(Options.Instance.DatabaseCredentials.ToString()))
            {
                conn.Open();

                using (NpgsqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"UPDATE {DB_DATABASE_LOCKPICKING}.{DB_SCHEMA_ATTRIBUTES}.{DB_TABLE_BELTS} SET name=@name,imagelink=@imagelink,description=@desc;";
                    cmd.Parameters.AddWithValue("@name", belt.Name);
                    cmd.Parameters.AddWithValue("@imagelink", belt.Imagelink);
                    cmd.Parameters.AddWithValue("@desc", belt.Description);

                    return cmd.ExecuteNonQuery() == 1;
                }
            }
        }

        public static bool DeleteBelt(Belt belt)
        {
            using (NpgsqlConnection conn = new(Options.Instance.DatabaseCredentials.ToString()))
            {
                conn.Open();

                using (NpgsqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"DELETE FROM {DB_DATABASE_LOCKPICKING}.{DB_SCHEMA_ATTRIBUTES}.{DB_TABLE_BELTS} WHERE id=@id;";
                    cmd.Parameters.AddWithValue("@id", belt.DatabaseId);

                    return cmd.ExecuteNonQuery() == 1;
                }
            }
        }
    }
}
