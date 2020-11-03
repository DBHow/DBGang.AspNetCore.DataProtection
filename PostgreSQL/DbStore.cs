using System.Collections.Generic;
using System.Data;
using Npgsql;

namespace DBGang.AspNetCore.DataProtection.PostgreSQL
{
    public static class DbStore
    {
        public static List<DataProtectionKey> GetAll(string connectionString)
        {
            var list = new List<DataProtectionKey>();
            var table = new DataTable();

            using var conn = new NpgsqlConnection(connectionString);
            using var comm = new NpgsqlCommand("SELECT id, name, value FROM data_protection_key", conn);
            using var adapter = new NpgsqlDataAdapter(comm);

            conn.Open();
            comm.Prepare();
            adapter.Fill(table);
            conn.Close();

            if (table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    list.Add(new DataProtectionKey
                    {
                        Id = (int)row["id"],
                        Name = (string)row["name"],
                        Value = (string)row["value"]
                    });

                }
            }

            return list;
        }

        public static void Save(string connectionString, DataProtectionKey key)
        {
            using var conn = new NpgsqlConnection(connectionString);
            using var comm = new NpgsqlCommand("INSERT INTO data_protection_key (name, value) VALUES (@name, @value)", conn);
            comm.Parameters.Add("name", NpgsqlTypes.NpgsqlDbType.Varchar, 1024).Value = key.Name;
            comm.Parameters.Add("value", NpgsqlTypes.NpgsqlDbType.Text).Value = key.Value;

            conn.Open();
            comm.Prepare();
            comm.ExecuteNonQuery();
            conn.Close();
        }
    }
}
