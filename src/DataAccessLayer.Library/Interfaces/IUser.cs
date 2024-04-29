using Model.Library;

namespace DataAccessLayer.Library.Interfaces
{
    internal interface IUser : ICrud<User>
    {
        public User? GetByUsernamePassword(string username, string password);
    }
}
