﻿using DataAccessLayer.Library;
using Model.Library;

namespace BusinessLogic.Library.Tests
{
    [TestClass()]
    public class ReservationHandlerTests
    {
        

        [TestMethod()]
        public void CreateReservationTest()
        {
            Reservation reservation = new Reservation()
            {
                StartDate = DateTime.Today,
                BookId = Guid.Parse("455961db-e840-4d3a-9bd0-8fcd63841a05"),
                UserId = Guid.Parse("8e7628de-7e6b-4ef1-a49d-11a036a4b5c6"),
                EndDate = DateTime.Today.AddDays(30),
            };

            DataTableAccess<Reservation> da = new();
            ReservationHandler handler = new(da);
            handler.Add(reservation);
        }

        [TestMethod()]
        public void GetByBookTest()
        {
            DataTableAccess<Reservation> da = new();
            ReservationHandler handler = new(da);
            Book b = new()
            {
                Title = "Harry Potter e la pietra filosofale",
                AuthorName = "Pippo",
                AuthorSurname = "Franco",
                PublishingHouse = "Salani"
            };

            // TODO: probabilmente bisogna rivalutare la ricerca del libro
            var reservations = handler.GetByBook(b).ToList();
            Assert.IsNotNull(reservations);
            Assert.IsTrue(reservations.Count == 1, $"Count: {reservations.Count}");
            Assert.IsTrue(reservations[0].BookId == Guid.Parse("455961db-e840-4d3a-9bd0-8fcd63841a05"));
        }

        [TestMethod()]
        public void GetByBookIdTest()
        {
            DataTableAccess<Reservation> da = new();
            ReservationHandler handler = new(da);

            Guid id = Guid.Parse("455961db-e840-4d3a-9bd0-8fcd63841a05");
            var reservations = handler.GetByBookId(id).ToList();
            Assert.IsNotNull(reservations);
            Assert.IsTrue(reservations.Count == 3, $"Count: {reservations.Count}");
            Assert.IsTrue(reservations[0].BookId == id);
        }

        [TestMethod()]
        public void GetByStartDateTest()
        {
            DataTableAccess<Reservation> da = new();
            ReservationHandler handler = new(da);

            var result = handler.GetByStartDate(new DateTime(2024, 04, 29)).ToList();
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count == 3);
        }

        [TestMethod()]
        public void GetByEndDateTest()
        {
            DataTableAccess<Reservation> da = new();
            ReservationHandler handler = new(da);

            var result = handler.GetByEndDate(new DateTime(2024, 03, 29)).ToList();
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count == 1, $"Count = {result.Count}");
        }

        [TestMethod()]
        public void GetByIntervalTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetByUserTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetByUserIdTest()
        {
            Assert.Fail();
        }
    }
}
