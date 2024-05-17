using Model.Library;

namespace DataAccessLayer.Library.Repository.Interfaces
{
    public interface IBookRepository
    {
        public bool Add(Book book);
        public bool Update(Book book);
        public bool Delete(Guid id);

        public Book? GetById(Guid id);
        public IEnumerable<Book> GetAll();
    }
}