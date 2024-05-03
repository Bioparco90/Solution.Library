using BusinessLogic.Library.Interfaces;
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
            UserHandler data = new(da);

            var user = data.GetByUsernamePassword(username,password);
            return user != null ? new() { Success = true, Message = string.Empty } : new() { Success = false, Message = "User not found" };
        }
    }
}
