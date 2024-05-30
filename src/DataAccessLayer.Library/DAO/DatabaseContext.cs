using DataAccessLayer.Library.DAO.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace DataAccessLayer.Library.DAO
{
    public class DatabaseContext :IOpenConnection
    {
        private readonly string _connectionString;

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
