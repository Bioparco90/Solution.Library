using DataAccessLayer.Library;
using Model.Library;

namespace BusinessLogic.Library
{
    public class Authentication : IAuthenticate
    {
        public Result CheckCredentials(string username, string password)
        {
            DataHandler<User> data = new(new DataTableAccess<User>());
            var user = data.Get(new() { Username = username, Password = password });
            if (user != null)
            {
                return new() { Success = true, Message = string.Empty };
            }
            else
            {
                return new() { Success = false, Message = "User not found" };
            }
        }
    }
}
