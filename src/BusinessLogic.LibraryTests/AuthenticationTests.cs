namespace BusinessLogic.Library.Tests
{
    [TestClass()]
    public class AuthenticationTests
    {
        [TestMethod("CheckCredentialsTrue")]
        public void CheckCredentialsTest1()
        {
            string username = "TestUser";
            string password = "123456";

            Authentication auth = new Authentication();
            Result result = auth.CheckCredentials(username, password);
            Console.WriteLine(result.Message);
            Assert.IsTrue(result.Success);
        }

        [TestMethod("CheckCredentialsFalse")]
        public void CheckCredentialsTest2()
        {
            string username = "TestUser";
            string password = "12345";

            Authentication auth = new Authentication();
            Result result = auth.CheckCredentials(username, password);
            Assert.IsFalse(result.Success);
        }
    }
}