using Model.Library;

namespace DataAccessLayer.Library.DAO.Interfaces
{
    public interface IUserDAO
    {
        User? GetById(Guid id);
        User? GetByUsername(string username);
        User? GetByUsernamePassword(string username, string password);
    }
}