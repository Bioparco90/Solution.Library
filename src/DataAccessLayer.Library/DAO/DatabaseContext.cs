using System.Data.SqlClient;

namespace DataAccessLayer.Library.DAO
{
    public class DatabaseContext
    {
        protected readonly string _connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=Library;Integrated Security=True;";

        public T DoWithOpenConnection<T>(Func<SqlConnection, T> action)
        {
            using SqlConnection conn = new(_connectionString);
            conn.Open();
            return action(conn);
        }
    }
}
