namespace BusinessLogic.Library.Interfaces
{
    public interface IAuthenticate
    {
        public bool Login(string username, string password);
        public void Logout();
    }
}