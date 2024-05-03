using Model.Library;

namespace BusinessLogic.Library.Interfaces
{
    public interface IAuthenticate
    {
        public Result CheckCredentials(string username, string password);
    }
}