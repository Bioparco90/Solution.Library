using BusinessLogic.Library.Authentication;
using BusinessLogic.Library.Exceptions;
using BusinessLogic.Library.Interfaces;
using DataAccessLayer.Library.Repository.Interfaces;
using Model.Library;
using System.Reflection;

namespace BusinessLogic.Library
{
    public class BookHandler : IBookHandler
    {
        private readonly Session _session;
        private readonly IBookRepository _bookRepository;

        public BookHandler(Session session, IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
            _session = session;
        }

        private Dictionary<string, object> BuildSearchParams(Book book, List<string> exclude)
        {
            Dictionary<string, object> result = new();

            PropertyInfo[] info = typeof(Book).GetProperties();
            foreach (var item in info)
            {
                var value = item.GetValue(book);
                if (value is not null && !exclude.Contains(item.Name))
                {
                    result.Add(item.Name, value);
                }
            }

            return result;
        }

        public bool Upsert(Book book)
        {
            return _session.RunWithAdminAuthorization(() =>
            {
                List<string> exclude = new() { "Id", "Quantity" };
                Dictionary<string, object> searchParams = BuildSearchParams(book, exclude);

                if (searchParams.Count != 4)
                {
                    return false;
                }

                var found = _bookRepository.GetByProperties(searchParams).ToList();

                if (found.Count == 0)
                {
                    return _bookRepository.Add(book);
                }
                else if (found.Count == 1)
                {
                    found[0].Quantity += book.Quantity;
                    return _bookRepository.Update(found[0]);
                }
                else
                {
                    return false;
                }
            });
        }

        public bool Update(Book book)
        {
            return _session.RunWithAdminAuthorization(() =>
            {
                List<string> exclude = new() { "Id", "Quantity" };
                Dictionary<string, object> searchParams = BuildSearchParams(book, exclude);

                if (searchParams.Count != 4)
                {
                    return false;
                }

                var found = _bookRepository.GetByProperties(searchParams).ToList();
                if (found.Count != 0)
                {
                    throw new BookSearchException("Book already present");
                }

                book.Id = found[0].Id;
                book.Quantity = found[0].Quantity;
                return _bookRepository.Update(book);
            });
        }
    }
}
