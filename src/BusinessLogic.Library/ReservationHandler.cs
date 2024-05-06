﻿using BusinessLogic.Library.Interfaces;
using DataAccessLayer.Library;
using Model.Library;

namespace BusinessLogic.Library
{
    public class ReservationHandler : GenericDataHandler<Reservation>, IReservation
    {
        public ReservationHandler(DataTableAccess<Reservation> dataAccess) : base(dataAccess)
        {
        }

        public bool Create(User user, Book book)
        {
            // 1: Check book existance
            DataTableAccess<Book> bookData = new();
            BookHandler bookHandler = new(bookData);
            var foundBook = bookHandler.GetSingleOrNull(book);
            if (foundBook is null)
            {
                return false;
            }

            // 2: Check if user has an active reservation for the book
            var activeReservations = CheckUserActiveReservations(user, foundBook).ToList();
            if (activeReservations.Count > 0)
            {
                return false;
            }


            // 3: Check book availability
            if (!IsAvailable(foundBook))
            {
                return false;
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

            return base.Add(reservation);
        }

        public bool EndReservation(User user, Book book)
        {
            // 1: check if book exists
            DataTableAccess<Book> bookData = new();
            BookHandler bookHandler = new(bookData);
            var foundBook = bookHandler.GetSingleOrNull(book);
            if (foundBook is null)
            {
                return false;
            }

            // 2: check if book has an active reservation (user)
            var activeReservations = CheckUserActiveReservations(user, foundBook).ToList();
            if (activeReservations.Count != 1)
            {
                return false;
            }

            // Update
            activeReservations[0].EndDate = DateTime.Now;
            return base.Update(activeReservations[0]);
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

        public bool IsAvailable(Book book)
        {
            var reservations = GetByBookId(book.Id)
                .Where(r => r.EndDate > DateTime.Now)
                .ToList();

            return reservations.Count < book.Quantity;
        }

        public IEnumerable<Reservation> GetByState(bool isActive)
        {
            return isActive switch
            {
                true => GetActives(),
                false => GetInactives()
            };
        }
        private IEnumerable<Reservation> GetActives() => GetAll().Where(r => r.EndDate > DateTime.Now);
        private IEnumerable<Reservation> GetInactives() => GetAll().Where(r => r.EndDate <= DateTime.Now);

        public IEnumerable<Reservation> GetByStateAndUser(bool isActive, Guid userId)
        {
            return isActive switch
            {
                true => GetActivesByUser(userId),
                false => GetInactivesByUser(userId)
            };
        }

        private IEnumerable<Reservation> GetActivesByUser(Guid id) => GetByUserId(id).Where(r => r.EndDate > DateTime.Now);
        private IEnumerable<Reservation> GetInactivesByUser(Guid id) => GetByUserId(id).Where(r => r.EndDate <= DateTime.Now);

        public IEnumerable<Reservation> CheckUserActiveReservations(User user, Book foundBook)
        {
            var activeReservations = GetByUserId(user.Id)
                .Where(r => foundBook.Id == r.BookId && r.EndDate > DateTime.Now)
                .ToList();

            return activeReservations;
        }

        public bool DeleteAll(IEnumerable<Reservation> listToDelete)
        {
            foreach (var item in listToDelete)
            {
                if (!base.Delete(item))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
