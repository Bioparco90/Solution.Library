using BusinessLogic.Library;

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
    }
}