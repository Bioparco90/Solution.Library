using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccessLayer.Library.Repository;
using DataAccessLayer.Library.Repository.Interfaces;

namespace DataAccessLayer.Library.Repository.Tests
{
    [TestClass()]
    public class ReservationRepositoryTests
    {

        IReservationRepository repo = new ReservationRepository(new(new()));

        [TestMethod()]
        public void GetAllTest()
        {
            var result = repo.GetAll().ToList();
            Assert.IsTrue(result.Any());
            Assert.IsTrue(result.Count == 76);
        }

        [TestMethod()]
        public void CreateTest()
        {
            Guid userId = Guid.Parse("1277e918-ac18-429f-b723-69806538593a");
            Guid bookId = Guid.Parse("98c1e100-771b-48bc-b8c4-a99fae77a36d");

            var result = repo.Create(userId, bookId);
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void UpdateTest()
        {
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
            Dictionary<string, object> parameters = new()
            {
                {"StartDate", DateTime.Now },
                {"EndDate", DateTime.Now }
            };

            Guid id = Guid.Parse("FCCB95E0-41C3-4160-97C0-E62A65C07860");
            var result = repo.Update(id, parameters);
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void GetActivesTest()
        {
            var data = repo.GetActives().ToList();

            Assert.AreEqual(5, data.Count);
        }

        [TestMethod()]
        public void GetActivesByBookTest()
        {
            var bookId = Guid.Parse("646519b1-c149-49ec-bfb9-8e23108c1ced");
            var data = repo.GetActives(bookId).ToList();

            Assert.AreEqual(1, data.Count);
            Assert.AreEqual("grace", data[0].Username.ToLower());
            Assert.AreEqual("angeli e demoni", data[0].Title.ToLower());
        }
    }
}
