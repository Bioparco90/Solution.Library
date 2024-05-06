using BusinessLogic.Library.Authentication.Interfaces;
using DataAccessLayer.Library;
using Model.Library;

namespace BusinessLogic.Library.Authentication
{
    public class Session : IAuthenticate
    {
        public bool IsAuthenticated { get; set; }
        public Guid UserId { get; set; }
        public string? LoggedUser { get; set; }
        public bool IsAdmin { get; set; }

        private static Session? Instance = null;

        private Session() { }

        public static Session GetInstance() => Instance ??= new();

        public bool Login(string username, string password)
        {
            var loginResult = CheckCredentials(username, password);
            if (!loginResult.Success)
            {
                return false;
            }

            UserId = loginResult.User.Id;
            LoggedUser = username;
            IsAdmin = loginResult.User.Role == Role.Admin;
            IsAuthenticated = true;

            return true;
        }

        public void Logout()
        {
            UserId = Guid.Empty;
            LoggedUser = string.Empty;
            IsAdmin = false;
            IsAuthenticated = false;
            Instance = null;
        }

        private static LoginResult CheckCredentials(string username, string password)
        {
            DataTableAccess<User> da = new();
            UserHandler data = new(da);

            var user = data.GetByUsernamePassword(username, password);
            if (user is null)
            {
                return new()
                {
                    Success = false,
                    Message = "User Not found"
                };
            }

            return new()
            {
                Success = true,
                Message = "User found",
                User = new()
                {
                    Id = user.Id,
                    Role = user.Role
                }
            };
        }
    }
}
