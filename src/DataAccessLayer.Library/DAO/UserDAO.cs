﻿using DataAccessLayer.Library.DAO.Interfaces;
using Model.Library;
using Model.Library.Enums;
using System.Data.SqlClient;

namespace DataAccessLayer.Library.DAO
{
    public class UserDAO : IUserDAO
    {
        private readonly IOpenConnection _db;

        public UserDAO(IOpenConnection db)
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

        public User? GetByUsername(string username)
        {
            return _db.DoWithOpenConnection(conn =>
            {
                string commandString = "SELECT Username FROM Users WHERE Username=@username";
                SqlCommand cmd = new(commandString, conn);
                cmd.Parameters.AddWithValue("@username", username);

                using SqlDataReader data = cmd.ExecuteReader();
                if (data.Read())
                {
                    return new User()
                    {
                        Username = data["Username"] as string ?? string.Empty,
                    };
                }
                return null;
            });
        }
    }
}
