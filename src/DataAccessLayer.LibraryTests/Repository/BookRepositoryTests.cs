using DataAccessLayer.Library.Repository.Interfaces;
using Model.Library;

namespace DataAccessLayer.Library.Repository.Tests
{


    [TestClass()]
    public class BookRepositoryTests
    {

        [TestMethod()]
        public void AddTest()
        {
            Book book = new()
            {
                Title = "TestTitle",
                AuthorName = "Name",
                AuthorSurname = "Surname",
                PublishingHouse = "House",
                Quantity = 5
            };

            IBookRepository repo = new BookRepository(new(new()));
            var result = repo.Add(book);

            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void GetByIdTest()
        {
            Guid id = Guid.Parse("ab579a40-a6e2-4018-a4ca-14bf0cd702d7");
            IBookRepository repo = new BookRepository(new(new()));
            var result = repo.GetById(id);

            Assert.IsNotNull(result);
            Assert.AreEqual("Harry Potter e il Principe Mezzosangue", result.Title);
        }

        [TestMethod()]
        public void UpdateTest()
        {
            IBookRepository repo = new BookRepository(new(new()));
            Guid id = Guid.Parse("98c1e100-771b-48bc-b8c4-a99fae77a36d");

            var bookFound = repo.GetById(id);
            Assert.IsNotNull(bookFound);

            Book newData = new()
            {
                Id = id,
                Title = "Topolino",
                AuthorName = "Walt",
                AuthorSurname = "Disney",
                PublishingHouse = "Panini",
                Quantity = 7
            };

            var result = repo.Update(newData);
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            IBookRepository repo = new BookRepository(new(new()));
            Guid id = Guid.Parse("98c1e100-771b-48bc-b8c4-a99fae77a36d");

            var result = repo.Delete(id);
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void GetAllTest()
        {
            IBookRepository repo = new BookRepository(new(new()));
            var result = repo.GetAll().ToList();

            Assert.IsTrue(result.Count == 17);
        }

        [TestMethod()]
        public void GetByPropertyTestNoParams()
        {
            IBookRepository repository = new BookRepository(new(new()));
            Dictionary<string, object> parameters = new();
            var result = repository.GetByProperties(parameters).ToList();

            Assert.AreEqual(17, result.Count);
        }

        [TestMethod()]
        public void GetByPropertyTestOneParam()
        {
            IBookRepository repository = new BookRepository(new(new()));
            Dictionary<string, object> parameters = new()
            {
                {"title", "Harry Potter" }
            };
            var result = repository.GetByProperties(parameters).ToList();

            Assert.AreEqual(7, result.Count);
        }

        [TestMethod()]
        public void GetByPropertyTestTwoParams()
        {
            IBookRepository repository = new BookRepository(new(new()));
            Dictionary<string, object> parameters = new()
            {
                {"AuthorName", "Dan" },
                {"Quantity", 3 }
            };
            var result = repository.GetByProperties(parameters).ToList();

            Assert.AreEqual(3, result.Count);
        }
    }
}