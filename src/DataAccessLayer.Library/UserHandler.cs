using Model.Library;
using System.Data;

namespace DataAccessLayer.Library
{
    public class UserHandler : GenericDataHandler<User>, IUser
    {
        public UserHandler(DataTableAccess<User> dataAccess) : base(dataAccess)
        {
        }

        public override User? Get(User user) => GetAll().FirstOrDefault(elem => elem.Username == user.Username);

        public User? GetByUsernamePassword(string username, string password) =>
            GetAll().FirstOrDefault(elem => elem.Username == username && elem.Password == password);

        public override bool Add(User user)
        {
            User? found = Get(user);
            if (found != null)
            {
                return false;
            }

            user.Id = Guid.NewGuid();
            bool added = base.Add(user);
            if (added)
            {
                // TODO: perchè salvo qua? Dovrei deresponsabilizzare questo metodo
                Save();
            }

            return added;
        }
    }
}
