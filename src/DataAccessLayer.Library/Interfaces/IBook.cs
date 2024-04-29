using Model.Library;

namespace DataAccessLayer.Library.Interfaces
{
    internal interface IBook
    {
        public bool AddMany(Book item, int quantity);
    }
}
