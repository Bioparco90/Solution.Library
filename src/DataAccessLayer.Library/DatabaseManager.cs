using System.Data.SqlClient;

namespace DataAccessLayer.Library
{
    public class DatabaseManager
    {
        public readonly string _connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=Library;Integrated Security=True;";

        public string CreateString(Dictionary<string, object> parameters)
        {
            List<string> k = new();
            foreach (KeyValuePair<string, object> kvp in parameters)
            {
                k.Add($" {kvp.Key}=@{kvp.Key.ToLower()}");
            }

            return string.Join(",", k);
        }

        public string CreateFilterString(Dictionary<string, object> parameters)
        {
            List<string> k = new();
            foreach (KeyValuePair<string, object> kvp in parameters)
            {
                k.Add($" {kvp.Key}=@{kvp.Key.ToLower() + "Filter"}");
            }

            return string.Join(" AND", k);
        }

        public void AddParameters(SqlCommand cmd, Dictionary<string, object> parameters)
        {
            foreach (KeyValuePair<string, object> kvp in parameters)
            {
                cmd.Parameters.AddWithValue(kvp.Key.ToLower(), kvp.Value);
            }
        }

        public void AddFilterParameters(SqlCommand cmd, Dictionary<string, object> parameters)
        {
            foreach (KeyValuePair<string, object> kvp in parameters)
            {
                cmd.Parameters.AddWithValue(kvp.Key + "Filter", kvp.Value);
            }
        }
    }
}
