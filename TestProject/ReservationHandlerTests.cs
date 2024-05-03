using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLogic.Library;
using DataAccessLayer.Library;
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
            SearchBooksParams book = new()
            {
                PublishingHouse = "Mondadori"
            };

            var reservations = handler.GetByBook(book)?.ToList();
            Assert.IsNotNull(reservations);
            Assert.IsTrue(reservations.Count == 4, $"Count: {reservations.Count}");
            Assert.IsTrue(reservations[0].BookId == Guid.Parse("8e3064be-8ac6-42a4-9f81-a3d895229a2f"));
            Assert.IsTrue(reservations[3].BookId == Guid.Parse("c44eb98e-ce1c-4544-985b-3d0d6e870ac6"));
        }

        [TestMethod()]
        public void GetByBookIdTest()
        {
            DataTableAccess<Reservation> da = new();
            ReservationHandler handler = new(da);

            Guid id = Guid.Parse("8e3064be-8ac6-42a4-9f81-a3d895229a2f");
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
            DataTableAccess<Reservation> da = new();
            ReservationHandler handler = new(da);

            DateTime start = new(2021, 01, 01);
            DateTime end = new(2023, 12, 31);
            var result = handler.GetByInterval(start, end).ToList();
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count == 2, $"Count = {result.Count}");
        }

        [TestMethod()]
        public void GetByUserTest()
        {
            DataTableAccess<Reservation> da = new();
            ReservationHandler handler = new(da);


            var user1 = "Admin1";
            var user2 = "User1";

            var reservations1 = handler.GetByUser(user1)?.ToList();
            var reservations2 = handler.GetByUser(user2)?.ToList();

            Assert.IsNotNull(reservations1);
            Assert.IsNotNull(reservations2);
            Assert.IsTrue(reservations1.Count == 3, $"Count = {reservations1.Count}");
            Assert.IsTrue(reservations2.Count == 3, $"Count = {reservations2.Count}");
        }

        [TestMethod()]
        public void GetByUserIdTest()
        {
            DataTableAccess<Reservation> da = new();
            ReservationHandler handler = new(da);

            var id = Guid.Parse("e40e7e4c-e27f-48d9-b1d2-98b64af79adb");
            var reservations = handler.GetByUserId(id).ToList();
            Assert.IsNotNull(reservations);
            Assert.IsTrue(reservations.Count == 3, $"Count: {reservations.Count}");
            reservations.ForEach(r => Assert.IsTrue(r.UserId == id, $"{r.UserId}"));
        }

        [TestMethod()]
        public void CreateTest()
        {
            DataTableAccess<Reservation> da = new();
            ReservationHandler handler = new(da);

            User user = new()
            {
                Id = Guid.Parse("e40e7e4c-e27f-48d9-b1d2-98b64af79adb"),
                Username = "Admin1",
            };

            Book book = new()
            {
                Title = "TestCreate",
                AuthorName = "Er",
                AuthorSurname = "Brasiliano",
                PublishingHouse = "Onlyfans"
            };

            Assert.IsTrue(handler.Create(user, book));
            handler.Save();
        }
    }
}
