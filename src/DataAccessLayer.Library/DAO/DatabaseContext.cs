using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace DataAccessLayer.Library.DAO
{
    public class DatabaseContext
    {
        // "Data Source=localhost\\SQLEXPRESS;Initial Catalog=Library;Integrated Security=True;"
        protected string _connectionString; // 

        public DatabaseContext()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            _connectionString = config.GetConnectionString("main")!;
        }

        public T DoWithOpenConnection<T>(Func<SqlConnection, T> action)
        {
            using SqlConnection conn = new(_connectionString);
            conn.Open();
            return action(conn);
        }
    }
}
