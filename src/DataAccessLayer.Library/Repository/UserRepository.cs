using DataAccessLayer.Library.DAO;
using DataAccessLayer.Library.Repository.Interfaces;
using Model.Library;

namespace DataAccessLayer.Library.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDAO _dao;

        public UserRepository(UserDAO dao)
        {
            _dao = dao;
        }

        public User? GetById(Guid id) => _dao.GetById(id);

        public User? GetByUsernamePassword(string username, string password) => _dao.GetByUsernamePassword(username, password);
    }
}
