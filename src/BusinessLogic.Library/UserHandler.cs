using BusinessLogic.Library.Interfaces;
using DataAccessLayer.Library.Repository.Interfaces;

namespace BusinessLogic.Library
{
    public class UserHandler : IUserHandler
    {
        private readonly IUserRepository _userRepository;

        public UserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool CheckUser(string username) => _userRepository.GetByUsername(username) is not null;
    }
}
