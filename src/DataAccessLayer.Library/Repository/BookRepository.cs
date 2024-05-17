using DataAccessLayer.Library.DAO;
using DataAccessLayer.Library.Repository.Interfaces;
using Model.Library;

namespace DataAccessLayer.Library.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookDAO _dao;

        public BookRepository(BookDAO dao)
        {
            _dao = dao;
        }

        public bool Add(Book book) => _dao.Add(book);

        public bool Update(Book book) => _dao.Update(book);

        public Book? GetById(Guid id) => _dao.GetById(id);
        
        public IEnumerable<Book> GetAll() => _dao.GetAll();

        public bool Delete(Guid id) => _dao.Delete(id);
    }
}
