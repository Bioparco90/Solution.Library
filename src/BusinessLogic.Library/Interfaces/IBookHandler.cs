using Model.Library;

namespace BusinessLogic.Library.Interfaces
{
    public interface IBookHandler
    {
        public bool Upsert(Book book);
    }
}