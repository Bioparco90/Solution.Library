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
        private readonly IReservationHandler _reservationHandler;

        public BookHandler(Session session, IBookRepository bookRepository, IReservationHandler reservationHandler)
        {
            _bookRepository = bookRepository;
            _session = session;
            _reservationHandler = reservationHandler;
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

        private bool BookNotExists(Book book) => SearchMany(book).ToList().Count == 0;

        public Book? SearchSingle(Book book) => SearchMany(book).ToList().SingleOrDefault();

        public Book SearchSingle(Book book, Func<int, bool> constraint)
        {
            List<string> exclude = new() { "Id", "Quantity" };
            Dictionary<string, object> searchParams = BuildSearchParams(book, exclude);

            if (!constraint(searchParams.Count))
            {
                throw new MandatoryFieldException("Please fill all fields");
            }

            return _bookRepository.GetByProperties(searchParams).ToList().SingleOrDefault() ?? throw new BookSearchException("Book not found");
        }

        public IEnumerable<Book> SearchMany(Book book)
        {
            List<string> exclude = new() { "Id", "Quantity" };
            Dictionary<string, object> searchParams = BuildSearchParams(book, exclude);

            return _bookRepository.GetByProperties(searchParams).ToList();
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
                return BookNotExists(book) ? _bookRepository.Update(book) : false;
            });
        }

        public bool Delete(Book book)
        {
            return _session.RunWithAdminAuthorization(() =>
            {
                var found = SearchSingle(book, parametersCount => parametersCount == 4);

                var activeReservations = _reservationHandler.GetActiveReservation(found.Id).ToList();
                var canDeleteBook = activeReservations.Count == 0;
                if (!canDeleteBook)
                {
                    throw new BookOnLoanException(activeReservations);
                }
                return _bookRepository.Delete(found.Id);
            });
        }
        public bool Loan(Book book)
        {
            var found = SearchSingle(book, parametersCount => parametersCount == 4);

            var actives = _reservationHandler.GetActiveReservation(found.Id).ToList();
            if (actives.Count >= found.Quantity)
            {
                var nextAvailable = actives.OrderByDescending(a => a.EndDate).First();
                throw new LoanLimitReachedException($"The reservation was not successful because the book {found.Title} is still reserved until {nextAvailable.EndDate.AddDays(1)}.");
            }

            if (actives.Any(a => a.Username == _session.LoggedUser))
            {
                throw new BookOnLoanException($"The user already has {found.Title} on loan.");
            }

            return _reservationHandler.CreateReservation(found.Id);
        }
    }
}
