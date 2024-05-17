﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccessLayer.Library.Repository;
using DataAccessLayer.Library.Repository.Interfaces;

namespace DataAccessLayer.Library.Repository.Tests
{
    [TestClass()]
    public class ReservationRepositoryTests
    {
        [TestMethod()]
        public void GetAllTest()
        {
            IReservationRepository repo = new ReservationRepository(new(new()));
            var result = repo.GetAll().ToList();
            Assert.IsTrue(result.Any());
            Assert.IsTrue(result.Count == 77);
        }

        [TestMethod()]
        public void CreateTest()
        {
            IReservationRepository repo = new ReservationRepository(new(new()));
            Guid userId = Guid.Parse("1277e918-ac18-429f-b723-69806538593a");
            Guid bookId = Guid.Parse("98c1e100-771b-48bc-b8c4-a99fae77a36d");

            var result = repo.Create(userId, bookId);
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void UpdateTest()
        {
            IReservationRepository repo = new ReservationRepository(new(new()));
            Dictionary<string, object> parameters = new()
            {
                {"EndDate", DateTime.Now }
            };

            Guid id = Guid.Parse("FCCB95E0-41C3-4160-97C0-E62A65C07860");
            var result = repo.Update(id, parameters);
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void UpdateTestWithMoreParameters()
        {
            IReservationRepository repo = new ReservationRepository(new(new()));
            Dictionary<string, object> parameters = new()
            {
                {"StartDate", DateTime.Now },
                {"EndDate", DateTime.Now }
            };

            Guid id = Guid.Parse("FCCB95E0-41C3-4160-97C0-E62A65C07860");
            var result = repo.Update(id, parameters);
            Assert.IsTrue(result);
        }
    }
}
