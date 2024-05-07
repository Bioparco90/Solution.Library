using BusinessLogic.Library.Authentication;
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

            Session auth = Session.GetInstance();
            var result = auth.Login(username, password);
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void CheckCredentialsTest2()
        {
            string username = "User1";
            string password = "12345";

            Session auth = Session.GetInstance();
            var result = auth.Login(username, password);
            Assert.IsFalse(result);
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
                Password = "franco"
            };

            DataTableAccess<User> da = new();
            UserHandler handler = new(da);

            Assert.IsTrue(handler.Delete(user));
        }


        // TODO: la logica di update è cambiata, ora il check sull'esistenza del singolo oggetto va fatta prima di passare l'oggetto (passare l'oggetto trovato)
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