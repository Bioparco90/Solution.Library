using BusinessLogic.Library;
using DataAccessLayer.Library;
using Model.Library;
using System.Data;
using System.Xml.Linq;

namespace TestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod()]
        public void CheckCredentialsTest1()
        {
            string username = "TestUser";
            string password = "123456";

            Authentication auth = new Authentication();
            Result result = auth.CheckCredentials(username, password);
            Console.WriteLine(result.Message);
            Assert.IsTrue(result.Success);
        }

        [TestMethod()]
        public void CheckCredentialsTest2()
        {
            string username = "TestUser2";
            string password = "12345";

            Authentication auth = new Authentication();
            Result result = auth.CheckCredentials(username, password);
            Console.WriteLine(result.Message);
            Assert.IsFalse(result.Success);
        }

        [TestMethod()]
        public void CreateUser()
        {
            User user = new User()
            {
                //Id = Guid.NewGuid(),
                Username = "TestUser2",
                Password = "123456",
                Role = Role.Admin,
            };
            DataTableAccess<User> da = new();
            DataTable dt = new();
            UserHandler handler = new(da, dt);

            Assert.IsTrue(handler.Add(user));
        }

        [TestMethod()]
        public void ReadUser()
        {
            DataTableAccess<User> da = new();
            DataTable dt = new();
            UserHandler handler = new(da, dt);

            int count = handler.GetAll().ToList().Count;
            Assert.IsTrue(count == 1);
        }

        [TestMethod()]
        public void DeleteUser()
        {
            User user = new()
            {
                Username = "TestUser",
                Password = "123456"
            };

            DataTableAccess<User> da = new();
            DataTable dt = new();
            UserHandler handler = new(da, dt);

            if (handler.Delete(user))
            {
                handler.Save();
                Assert.IsTrue(true);
            }
            else
            {
                Assert.IsTrue(false);
            }

        }

        [TestMethod()]
        public void UpdateUser()
        {
            User user = new()
            {
                Username = "TestUser",
                //Password = "123456",
                Role = Role.User,
            };

            DataTableAccess<User> da = new();
            DataTable dt = new();
            UserHandler handler = new(da, dt);
            var res = handler.Update(user);
            handler.Save();
            Assert.IsTrue(res);
        }
    }
}