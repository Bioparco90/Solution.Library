using BusinessLogic.Library.Exceptions;
using BusinessLogic.Library.Interfaces;
using DataAccessLayer.Library.DAO;
using DataAccessLayer.Library.Repository;
using DataAccessLayer.Library.Repository.Interfaces;
using Model.Library;
using Model.Library.Enums;

namespace BusinessLogic.Library.Authentication
{
    public class Session : IAuthenticate
    {
        private static readonly IUserRepository _userRepository = new UserRepository(new UserDAO(new DatabaseContext()));
        public bool IsAuthenticated { get; set; }
        public Guid UserId { get; set; }
        public string? LoggedUser { get; set; }
        public bool IsAdmin { get; set; }

        private static Session? Instance = null;

        private Session()
        {
        }

        public static Session GetInstance() => Instance ??= new();

        public bool Login(string username, string password)
        {
            var loginResult = CheckCredentials(username, password);
            if (!loginResult.Success || loginResult.User is null)
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
            var user = _userRepository.GetByUsernamePassword(username, password);
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

        public T RunWithAdminAuthorization<T>(Func<T> action)
        {
            CheckAutorizations();
            return action();
        }

        private void CheckAutorizations()
        {
            if (Instance == null)
            {
                throw new NullReferenceException("Instance is not initialized");
            }

            if (!IsAuthenticated)
            {
                throw new SessionNotStartedException("User not authenticated.");
            }

            if (!IsAdmin)
            {
                throw new UnauthorizedUserException("Unauthorized access. This operation requires admin privileges.");
            }
        }
    }
}
