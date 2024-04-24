using Model.Library;
using System.Data;

namespace DataAccessLayer.Library
{
    public class UserHandler : DataHandler<User>
    {
        public UserHandler(DataTableAccess<User> dataAccess, DataTable table) : base(dataAccess)
        {
        }

        public override User? Get(User user) => GetAll().FirstOrDefault(elem => elem.Username == user.Username);

        public User? GetByUsernamePassword(string username, string password) =>
            GetAll().FirstOrDefault(elem => elem.Username == username && elem.Password == password);

        // verificare esistenza item
        // se non esiste, aggiungere
        // se esiste fare update della quantità
        // valutare restituzione bool
        public override bool Add(User user)
        {
            User? found = Get(user);
            if (found != null)
            {
                return false;
            }
            //user.Id = Guid.NewGuid();
            bool added = base.Add(user);
            if (!added)
            {
                return false;
            }
            else
            {
                Save();
                return true;
            }
        }
    }
}
