using Model.Library;

namespace DataAccessLayer.Library.Repository.Interfaces
{
    public interface IUserRepository
    {
        public User? GetById(Guid id);
        User? GetByUsername(string username);
        public User? GetByUsernamePassword(string username, string password);
    }
}