using Model.Library;

namespace DataAccessLayer.Library.DAO.Interfaces
{
    public interface IBookDAO
    {
        bool Add(Book book);
        bool Update(Book book);
        bool Delete(Guid id);

        IEnumerable<Book> GetAll();
        Book? GetById(Guid id);
        IEnumerable<Book> GetByProperties(Dictionary<string, object> properties);
    }
}