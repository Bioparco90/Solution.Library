namespace BusinessLogic.Library.V1.Authentication.Interfaces
{
    public interface IAuthenticate
    {
        public bool Login(string username, string password);
        public void Logout();
    }
}