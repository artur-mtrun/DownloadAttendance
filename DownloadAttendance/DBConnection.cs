using Npgsql;
using System.Data;

namespace DownloadAttendance
{
    public class DBConnection
    {
        public static bool RunDBQuery(string query)
        {
            using (NpgsqlConnection connection = GetConnection())
            {
                connection.Open();
                if (connection.State == ConnectionState.Open)
                {
                    try
                    {
                        NpgsqlCommand DBQuerry = new NpgsqlCommand(query, connection);
                        DBQuerry.ExecuteNonQuery();
                        connection.Close();
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
                else
                    return false;
            }
        }
        public static DataTable GetTerminalsFromDB()
        {
            var query = "SELECT * FROM public.machines";
            return GetDataFromDB(query);
        }
        public static NpgsqlConnection GetConnection()
        {
            var connectionStr = GetConnString();
            return new NpgsqlConnection(connectionStr);
        }
        private static string GetConnString()
        {
            string line;
            // Read the file and display it line by line.
            System.IO.StreamReader file = new System.IO.StreamReader("dblp");
            if ((line = file.ReadLine()) == null)
                line = "@";
            file.Close();
            return line;
        }
        public static DataTable GetDataFromDB(string query)
        {
            DataTable dataTable = new DataTable();
            using (NpgsqlConnection connection = GetConnection())
            {
                connection.Open();
                if (connection.State == ConnectionState.Open)
                {
                    NpgsqlCommand npgsqlCommand = new NpgsqlCommand(query, connection);
                    NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader();
                    if (npgsqlDataReader.HasRows)
                    {
                        dataTable.Load(npgsqlDataReader);
                    }
                    npgsqlCommand.Dispose();
                    connection.Close();
                }

                return dataTable;
            }
        }
    }
}
