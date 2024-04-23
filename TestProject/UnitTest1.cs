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
            string username = "TestUser";
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
                UserId = Guid.NewGuid(),
                Username = "TestUser",
                Password = "123456",
                Role = Role.Admin,
            };

            DataHandler<User> handler = new(new DataTableAccess<User>());
            handler.Add(user);
            Assert.IsTrue(true);
        }

        [TestMethod()]
        public void ReadUser()
        {
            DataHandler<User> handler = new(new DataTableAccess<User>());
            int count = handler.GetAll().ToList().Count;
            Assert.IsTrue(count  == 1);
        }
    }
}