using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using Npgsql;

namespace LockpickersGuide.Logic
{
    internal static partial class Database
    {
        internal static bool AreDatabaseCredentialsValid()
        {
            if (!Options.Instance.DatabaseCredentials.Host.IsSet() || !Options.Instance.DatabaseCredentials.Username.IsSet() || !Options.Instance.DatabaseCredentials.Password.IsSet() || !Options.Instance.DatabaseCredentials.Port.IsSet())
            {
                return false;
            }

            using (NpgsqlConnection conn = new(Options.Instance.DatabaseCredentials.ToString()))
            {
                try
                {
                    conn.Open();

                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        return true;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }

            return false;
        }
    }
}
