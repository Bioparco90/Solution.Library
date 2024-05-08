using BusinessLogic.Library.Interfaces;
using BusinessLogic.Library.Types;
using DataAccessLayer.Library;
using Model.Library;
using Model.Library.Enums;

namespace BusinessLogic.Library
{
    public class ReservationHandler : GenericDataHandler<Reservation>, IReservation
    {
        public ReservationHandler(DataTableAccess<Reservation> dataAccess) : base(dataAccess)
        {
        }

        public ReservationResult Create(User user, Book book)
        {
            // 1: Check book existance
            DataTableAccess<Book> bookData = new();
            BookHandler bookHandler = new(bookData);
            var foundBook = bookHandler.GetSingleOrNull(book);
            if (foundBook is null)
            {
                return new() { StatusCode = ResultStatus.BookNotFound, Message = "Book not Found" };
            }

            // 2: Check if user has an active reservation for the book
            var activeReservations = CheckUserActiveReservations(user, foundBook).ToList();
            if (activeReservations.Count > 0)
            {
                return new() { StatusCode = ResultStatus.BookOnLoan, Message = "The user already has a copy of the book on loan" };
            }

            // 3: Check book availability
            var actives = GetActiveReservationsForBook(foundBook.Id).ToList();
            if (actives.Count >= foundBook.Quantity)
            {
                var nextAvailable = actives.OrderBy(r => r.EndDate).First();
                return new()
                {
                    StatusCode = ResultStatus.BookOnLoan,
                    Message = $"The reservation was not successful as the book {foundBook.Title} is still booked until {nextAvailable.EndDate.ToShortDateString()}"
                };
            }

            // Create the reservation
            Reservation reservation = new()
            {
                Id = Guid.NewGuid(),
                BookId = foundBook.Id,
                UserId = user.Id,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(30),
            };

            if (!base.Add(reservation))
            {
                return new() { StatusCode = ResultStatus.Error, Message = "Error occurred during create operations" };
            }

            return new() { StatusCode = ResultStatus.Success, Message = "Book booked successfully" };
        }

        public ReservationResult EndReservation(User user, Book book)
        {
            // 1: check if book exists
            DataTableAccess<Book> bookData = new();
            BookHandler bookHandler = new(bookData);
            var foundBook = bookHandler.GetSingleOrNull(book);
            if (foundBook is null)
            {
                return new() { StatusCode = ResultStatus.BookNotFound, Message = "Book not found" };
            }

            // 2: check if book has an active reservation (user)
            var activeReservations = CheckUserActiveReservations(user, foundBook).ToList();
            if (activeReservations.Count != 1)
            {
                return new() { StatusCode = ResultStatus.BookNotOnLoan, Message = $"The book {foundBook.Title} is currently not on loan." };
            }

            // Update
            activeReservations[0].EndDate = DateTime.Now;
            try
            {
                base.Update(activeReservations[0]);
                return new() { StatusCode = ResultStatus.Success, Message = "Reservation Updated" };
            }
            catch
            {
                return new() { StatusCode = ResultStatus.Error, Message = "Something goes wrong during update operations" };
            }
        }

        // TODO: Probabilmente andrà cancellato, c'è il metodo Create che è più corretto
        public override bool Add(Reservation item)
        {
            var reservation = GetSingleOrNull(item);
            if (reservation != null)
            {
                return false;
            }

            item.Id = Guid.NewGuid();
            return base.Add(item);
        }

        // TODO: Verificare eventualità di valori null in res (nel caso, filtrare)
        public IEnumerable<Reservation> GetByBook(SearchBooksParams book)
        {
            DataTableAccess<Book> da = new();
            BookHandler books = new(da);

            var booksFound = books.GetByProperties(book).ToList() ?? [];

            List<Reservation> res = new();
            booksFound.ForEach(book => res.AddRange(GetByBookId(book.Id)));

            return res;
        }

        public IEnumerable<Reservation> GetByBookId(Guid bookId) => GetAll().Where(r => r.BookId == bookId);

        public IEnumerable<Reservation> GetByStartDate(DateTime start) => GetAll().Where(r => r.StartDate == start);

        public IEnumerable<Reservation> GetByEndDate(DateTime end) => GetAll().Where(r => r.EndDate == end);

        public IEnumerable<Reservation> GetByInterval(DateTime start, DateTime end) => GetAll().Where(r => r.StartDate >= start && r.StartDate <= end);

        public IEnumerable<Reservation>? GetByUser(string username)
        {
            DataTableAccess<User> da = new();
            UserHandler users = new(da);
            var user = users.GetSingleOrNull(new() { Username = username });
            if (user is null)
            {
                return null;
            }

            return GetByUserId(user.Id);
        }

        public IEnumerable<Reservation> GetByUserId(Guid userId) => GetAll().Where(r => r.UserId == userId);

        private IEnumerable<Reservation> GetActiveReservationsForBook(Guid bookId) => GetByBookId(bookId).Where(r => r.EndDate > DateTime.Now);

        private IEnumerable<Reservation> CheckUserActiveReservations(User user, Book foundBook) => GetByUserId(user.Id).Where(r => foundBook.Id == r.BookId && r.EndDate > DateTime.Now);

        public bool DeleteAll(IEnumerable<Reservation> listToDelete)
        {
            try
            {
                foreach (var item in listToDelete)
                {
                    base.Delete(item);
                }
                return true;
            }
            catch
            {

                return false;
            }
        }
    }
}
