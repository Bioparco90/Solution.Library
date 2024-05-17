using Model.Library;
using Model.Library.Enums;
using System.Data.SqlClient;

namespace DataAccessLayer.Library.DAO
{
    public class UserDAO
    {
        private readonly DatabaseContext _db;

        public UserDAO(DatabaseContext db)
        {
            _db = db;
        }

        public User? GetById(Guid id)
        {
            return _db.DoWithOpenConnection(conn =>
            {
                string commandString = "SELECT Username, Role FROM Users WHERE ID=@id";
                SqlCommand cmd = new(commandString, conn);
                cmd.Parameters.AddWithValue("@id", id);

                using SqlDataReader data = cmd.ExecuteReader();
                if (data.Read())
                {
                    return new User()
                    {
                        Id = id,
                        Username = data["Username"] as string,
                        Role = (Role)data["Role"]
                    };
                }
                return null;
            });
        }

        public User? GetByUsernamePassword(string username, string password)
        {
            return _db.DoWithOpenConnection(conn =>
            {
                string commandString = "SELECT ID, Username, Role FROM Users WHERE Username=@username AND Password=@password";
                SqlCommand cmd = new(commandString, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);

                using SqlDataReader data = cmd.ExecuteReader();
                if (data.Read())
                {
                    return new User()
                    {
                        Id = (Guid)data["ID"],
                        Username = data["Username"] as string,
                        Role = (Role)data["Role"]
                    };
                }
                return null;
            });
        }
    }
}
