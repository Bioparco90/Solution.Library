using Model.Library;

namespace BusinessLogic.Library.V1.Interfaces
{
    internal interface IUser : ICrud<User>
    {
        public User? GetByUsernamePassword(string username, string password);
    }
}
