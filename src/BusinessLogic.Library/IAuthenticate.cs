using Model.Library;

namespace BusinessLogic.Library
{
    public interface IAuthenticate
    {
        public Result CheckCredentials(string username, string password);
    }
}