using BusinessLogic.Library;
using DataAccessLayer.Library;
using Model.Library;

namespace TestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod()]
        public void CheckCredentialsTest1()
        {
            string username = "TestUser2f";
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
                Username = "TestUser",
                Password = "123456",
                Role = Role.Admin,
            };
            DataTableAccess<User> da = new();
            UserHandler handler = new(da);

            Assert.IsTrue(handler.Add(user));
        }

        [TestMethod()]
        public void ReadUser()
        {
            DataTableAccess<User> da = new();
            UserHandler handler = new(da);

            int count = handler.GetAll().ToList().Count;
            Assert.IsTrue(count == 1);
        }

        [TestMethod()]
        public void DeleteUser()
        {
            User user = new()
            {
                Username = "TestUser2f",
                Password = "123456"
            };

            DataTableAccess<User> da = new();
            UserHandler handler = new(da);

            Assert.IsTrue(handler.Delete(user));
        }

        [TestMethod()]
        public void UpdateUser()
        {
            User user = new()
            {
                Username = "TestUser2f",
                //Password = "123456",
                Role = Role.User,
            };

            DataTableAccess<User> da = new();
            UserHandler handler = new(da);
            Assert.IsTrue(handler.Update(user));
        }

        [TestMethod()]
        public void DeleteAll()
        {
            DataTableAccess<User> da = new();
            UserHandler handler = new(da);

            Assert.IsTrue(handler.DeleteAll());
        }
    }
}