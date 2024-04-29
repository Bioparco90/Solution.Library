﻿using BusinessLogic.Library.Interfaces;
using DataAccessLayer.Library;
using Model.Library;

namespace BusinessLogic.Library
{
    public class UserHandler : GenericDataHandler<User>, IUser
    {
        public UserHandler(DataTableAccess<User> dataAccess) : base(dataAccess)
        {
        }

        public override User? Get(User user) => GetAll().FirstOrDefault(elem => elem.Username == user.Username);

        public User? GetByUsernamePassword(string username, string password) =>
            GetAll().FirstOrDefault(elem => elem.Username == username && elem.Password == password);

        public override bool Add(User user)
        {
            User? found = Get(user);
            if (found != null)
            {
                return false;
            }

            user.Id = Guid.NewGuid();
            return base.Add(user);
        }
    }
}