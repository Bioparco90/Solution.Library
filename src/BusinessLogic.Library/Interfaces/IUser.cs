using Model.Library;

namespace BusinessLogic.Library.Interfaces
{
    internal interface IUser : ICrud<User>
    {
        public User? GetByUsernamePassword(string username, string password);
    }
}
