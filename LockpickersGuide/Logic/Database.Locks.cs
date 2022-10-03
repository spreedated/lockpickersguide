using LockpickersGuide.Models;
using LockpickersGuide.ViewLogic;
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
        public static IEnumerable<Lock> GetLocks()
        {
            using (NpgsqlConnection conn = new(Options.Instance.DatabaseCredentials.ToString()))
            {
                conn.Open();

                using (NpgsqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"SELECT * FROM {DB_DATABASE_LOCKPICKING}.{DB_SCHEMA_ATTRIBUTES}.{DB_TABLE_LOCKS};";

                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            yield return GetLocksFromDatabaseReader(dr);
                        }
                    }
                }

                conn.Close();
            }
        }

        public static IEnumerable<Lock> GetLocks(IEnumerable<FilterOption> filterOptions)
        {
            if (!filterOptions.IsSet())
            {
                yield break;
            }

            using (NpgsqlConnection conn = new(Options.Instance.DatabaseCredentials.ToString()))
            {
                conn.Open();

                using (NpgsqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"SELECT * FROM {DB_DATABASE_LOCKPICKING}.{DB_SCHEMA_ATTRIBUTES}.{DB_TABLE_LOCKS};";

                    foreach (FilterOption f in filterOptions)
                    {
                        cmd.Parameters.AddWithValue(f.Name, f.Value);
                    }

                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            yield return GetLocksFromDatabaseReader(dr);
                        }
                    }
                }

                conn.Close();
            }
        }

        private static Lock GetLocksFromDatabaseReader(NpgsqlDataReader dr)
        {
            return new()
            {
                DatabaseId = dr.GetInt32(0),
                Type = ObjectStorage.Locktypes.FirstOrDefault(x => x.DatabaseId == dr.GetInt32(1)),
                Brand = ObjectStorage.Brands.FirstOrDefault(x => x.DatabaseId == dr.GetInt32(2)),
                Modelname = dr.IsDBNull(3) ? null : dr.GetString(3),
                Model = dr.IsDBNull(4) ? null : dr.GetString(4),
                Core = ObjectStorage.Cores.FirstOrDefault(x => x.DatabaseId == dr.GetInt32(5)),
                Description = dr.IsDBNull(6) ? null : dr.GetString(6),
                Keycount = dr.IsDBNull(7) ? null : dr.GetInt32(7),
                Gutable = dr.IsDBNull(8) ? null : dr.GetBoolean(8),
                Link = dr.GetString(9),
                Imagelink = dr.GetString(10),
                Color = ObjectStorage.Colors.FirstOrDefault(x=> x.DatabaseId == dr.GetInt32(11))
            };
        }

        public static int AddLocks(Belt belt)
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

        public static bool UpdateLocks(Belt belt)
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

        public static bool DeleteLockCollection(Belt belt)
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
