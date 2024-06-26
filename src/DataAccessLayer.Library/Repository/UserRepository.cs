﻿using DataAccessLayer.Library.DAO.Interfaces;
using DataAccessLayer.Library.Repository.Interfaces;
using Model.Library;

namespace DataAccessLayer.Library.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IUserDAO _dao;

        public UserRepository(IUserDAO dao)
        {
            _dao = dao;
        }

        public User? GetById(Guid id) => _dao.GetById(id);
        public User? GetByUsername(string username) => _dao.GetByUsername(username);
        public User? GetByUsernamePassword(string username, string password) => _dao.GetByUsernamePassword(username, password);
    }
}
