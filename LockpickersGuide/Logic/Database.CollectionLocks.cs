using LockpickersGuide.Models;
using Npgsql;
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
        public static IEnumerable<CollectionLocks> GetCollectionLocks()
        {
            using (NpgsqlConnection conn = new(Options.Instance.DatabaseCredentials.ToString()))
            {
                conn.Open();

                using (NpgsqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"SELECT * FROM {DB_DATABASE_LOCKPICKING}.{DB_SCHEMA_COLLECTION}.{DB_TABLE_LOCKS};";

                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            yield return GetCollectionLocksFromDatabaseReader(dr);
                        }
                    }
                }

                conn.Close();
            }
        }

        private static CollectionLocks GetCollectionLocksFromDatabaseReader(NpgsqlDataReader dr)
        {
            return new()
            {
                DatabaseId = dr.GetString(0),
                Type = ObjectStorage.Locktypes.FirstOrDefault(x => x.DatabaseId == dr.GetInt32(1)),
                Brand = ObjectStorage.Brands.FirstOrDefault(x => x.DatabaseId == dr.GetInt32(2)),
                Modelname = dr.IsDBNull(3) ? null : dr.GetString(3),
                Model = dr.IsDBNull(4) ? null : dr.GetString(4),
                BindingOrder = dr.IsDBNull(5) ? null : dr.GetString(5),
                Picked = dr.GetBoolean(6),
                Core = ObjectStorage.Cores.FirstOrDefault(x => x.DatabaseId == dr.GetInt32(7)),
                Description = dr.IsDBNull(8) ? null : dr.GetString(8),
                Keycount = dr.GetInt32(9),
                Price = dr.GetDecimal(10),
                Ownership = dr.GetBoolean(11),
                Guttable = dr.GetBoolean(12)
            };
        }

        public static int AddCollectionLocks(Belt belt)
        {
            throw new NotImplementedException();

            using (NpgsqlConnection conn = new(Options.Instance.DatabaseCredentials.ToString()))
            {
                conn.Open();

                using (NpgsqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"INSERT INTO {DB_DATABASE_LOCKPICKING}.{DB_SCHEMA_ATTRIBUTES}.{DB_TABLE_BELTS} (name,imagelink,description) VALUES (@name,@imagelink,@desc) RETURNING id;";
                    cmd.Parameters.AddWithValue("@name", belt.Name);
                    cmd.Parameters.AddWithValue("@imagelink", belt.Imagelink);
                    cmd.Parameters.AddWithValue("@desc", belt.Description);

                    int databaseId = (int)cmd.ExecuteScalar();

                    return databaseId;
                }
            }
        }

        public static bool UpdateCollectionLocks(Belt belt)
        {
            throw new NotImplementedException();

            using (NpgsqlConnection conn = new(Options.Instance.DatabaseCredentials.ToString()))
            {
                conn.Open();

                using (NpgsqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"UPDATE {DB_DATABASE_LOCKPICKING}.{DB_SCHEMA_ATTRIBUTES}.{DB_TABLE_BELTS} SET name=@name,imagelink=@imagelink,description=@desc WHERE id = @id;";
                    cmd.Parameters.AddWithValue("@name", belt.Name);
                    cmd.Parameters.AddWithValue("@imagelink", belt.Imagelink);
                    cmd.Parameters.AddWithValue("@desc", belt.Description);
                    cmd.Parameters.AddWithValue("@id", belt.DatabaseId);

                    return cmd.ExecuteNonQuery() == 1;
                }
            }
        }

        public static bool DeleteCollectionLockCollection(Belt belt)
        {
            throw new NotImplementedException();

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
