namespace BusinessLogic.Library.Authentication.Interfaces
{
    public interface IAuthenticate
    {
        public bool Login(string username, string password);
    }
}