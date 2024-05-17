using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccessLayer.Library.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Library.Repository.Interfaces;
using Model.Library.Enums;

namespace DataAccessLayer.Library.Repository.Tests
{
    [TestClass()]
    public class UserRepositoryTests
    {
        [TestMethod()]
        public void GetByIdTest()
        {
            Guid id = Guid.Parse("1277e918-ac18-429f-b723-69806538593a");
            IUserRepository repo = new UserRepository(new(new()));
            var result = repo.GetById(id);

            Assert.IsNotNull(result);
            Assert.AreEqual("alice", result.Username);
        }

        [TestMethod()]
        public void GetByUsernamePasswordTest()
        {
            IUserRepository repo = new UserRepository(new(new()));
            Guid id = Guid.Parse("1277e918-ac18-429f-b723-69806538593a");
            string username = "alice";
            string password = "password123";
            Role role = Role.User;

            var result = repo.GetByUsernamePassword(username, password);

            Assert.IsNotNull(result);
            Assert.AreEqual(id, result.Id);
            Assert.AreEqual(username, result.Username);
            Assert.AreEqual(role, result.Role);
        }
    }
}