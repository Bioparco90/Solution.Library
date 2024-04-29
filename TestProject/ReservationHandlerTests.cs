using DataAccessLayer.Library;
using Model.Library;

namespace BusinessLogic.Library.Tests
{
    [TestClass()]
    public class ReservationHandlerTests
    {
        [TestMethod()]
        public void CreateFile()
        {
            List<Reservation> reservations = new List<Reservation>
        {
            new Reservation
            {
                UserId = new Guid("8e7628de-7e6b-4ef1-a49d-11a036a4b5c6"),
                BookId = new Guid("455961db-e840-4d3a-9bd0-8fcd63841a05"),
                StartDate = new DateTime(2024, 04, 29),
                EndDate = new DateTime(2024, 05, 29)
            },
            new Reservation
            {
                UserId = new Guid("4a97af1c-9cb0-4ca9-9491-0dbeaf4cf1f6"),
                BookId = new Guid("455961db-e840-4d3a-9bd0-8fcd63841a05"),
                StartDate = new DateTime(2024, 04, 29),
                EndDate = new DateTime(2024, 05, 29)
            },
            new Reservation
            {
                UserId = new Guid("4a97af1c-9cb0-4ca9-9491-0dbeaf4cf1f6"),
                BookId = new Guid("455961db-e840-4d3a-9bd0-8fcd63841a06"),
                StartDate = new DateTime(2024, 02, 29),
                EndDate = new DateTime(2024, 03, 29)
            },
            new Reservation
            {
                UserId = new Guid("4a97af1c-9cb0-4ca9-9491-0dbeaf4cf1f6"),
                BookId = new Guid("64b6b079-4889-4d46-afd0-5d02491b3b7c"),
                StartDate = new DateTime(2023, 07, 29),
                EndDate = new DateTime(2023, 08, 29)
            },
            new Reservation
            {
                UserId = new Guid("a02a848b-e453-4903-a5f0-3d14a133abf6"),
                BookId = new Guid("455961db-e840-4d3a-9bd0-8fcd63841a05"),
                StartDate = new DateTime(2021, 01, 29),
                EndDate = new DateTime(2021, 02, 28)
            },
            new Reservation
            {
                UserId = new Guid("a02a848b-e453-4903-a5f0-3d14a133abf6"),
                BookId = new Guid("8bb10e3f-6d7e-4b5c-bc5b-69ed768ea37e"),
                StartDate = new DateTime(2024, 04, 29),
                EndDate = new DateTime(2024, 05, 29)
            }
        };

            DataTableAccess<Reservation> da = new();
            ReservationHandler handler = new(da);
            reservations.ForEach(r => handler.Add(r));
            handler.Save();
        }

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
            //handler.Save();
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

        // TODO: create tests
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
