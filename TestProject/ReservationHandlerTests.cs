using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLogic.Library;
using DataAccessLayer.Library;
using Model.Library;
using System.Reflection;

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
                StartDate = DateTime.Now,
                BookId = Guid.Parse("455961db-e840-4d3a-9bd0-8fcd63841a05"),
                UserId = Guid.Parse("8e7628de-7e6b-4ef1-a49d-11a036a4b5c6"),
                EndDate = DateTime.Now.AddDays(30),
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

            var reservations = handler.GetByBook(b).ToList();
            Assert.IsNotNull(reservations);
            Assert.IsTrue(reservations.Count == 1);
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
            Assert.IsTrue(reservations.Count == 1);
            Assert.IsTrue(reservations[0].BookId == id);
        }
    }
}