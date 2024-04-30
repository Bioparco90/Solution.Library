using DataAccessLayer.Library;
using Model.Library;

namespace BusinessLogic.Library.Tests
{
    [TestClass]
    public class UserTests
    {
        [TestMethod()]
        public void CheckCredentialsTest1()
        {
            string username = "Admin1";
            string password = "pippo";

            Authentication auth = new Authentication();
            Result result = auth.CheckCredentials(username, password);
            Assert.IsTrue(result.Success);
        }

        [TestMethod()]
        public void CheckCredentialsTest2()
        {
            string username = "User1";
            string password = "12345";

            Authentication auth = new Authentication();
            Result result = auth.CheckCredentials(username, password);
            Assert.IsFalse(result.Success);
        }

        [TestMethod()]
        public void ReadUser()
        {
            DataTableAccess<User> da = new();
            UserHandler handler = new(da);

            int count = handler.GetAll().ToList().Count;
            Assert.IsTrue(count == 2);
        }

        [TestMethod()]
        public void DeleteUser()
        {
            User user = new()
            {
                Username = "User1",
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
                Username = "Admin1",
                Role = Role.User,
            };

            DataTableAccess<User> da = new();
            UserHandler handler = new(da);

            Assert.IsTrue(handler.Update(user));
        }
    }
}