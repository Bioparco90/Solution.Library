using System.Data.SqlClient;

namespace DataAccessLayer.Library.DAO.Interfaces
{
    public interface IOpenConnection
    {
        T DoWithOpenConnection<T>(Func<SqlConnection, T> action);
    }
}