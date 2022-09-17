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
    }
}
