using DataAccessLayer.Library;
using Model.Library;
using System.Data;

namespace BusinessLogic.Library
{
    public class Authentication : IAuthenticate
    {
        public Result CheckCredentials(string username, string password)
        {
            DataTableAccess<User> da = new();
            DataTable dt = new();
            DataHandler<User> data = new(da, dt);

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
