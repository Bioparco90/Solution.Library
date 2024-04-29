using DataAccessLayer.Library;
using Model.Library;

namespace TestProject
{
    [TestClass]
    public class TestBook
    {
        [TestMethod()]
        public void CreateBook()
        {
            Book book = new Book()
            {
                Title = "Harry Potter",
                Name = "Pippo",
                Surname = "Franco",
                PublishingHouse = "Salani"
            };
            DataTableAccess<Book> da = new();
            BookHandler handler = new(da);
            Assert.IsTrue(handler.Add(book));
            handler.Save();
        }

        [TestMethod()]
        public void DeleteBook()
        {
            Book book = new Book()
            {
                Title = "Harry Potter",
                Name = "Pippo",
                Surname = "Franco",
                PublishingHouse = "Salani"

            };

            DataTableAccess<Book> da = new();
            BookHandler handler = new(da);
            Assert.IsTrue(handler.Delete(book));
            handler.Save();
        }

        [TestMethod()]
        public void AddManyOfExisting()
        {
            Book book = new Book()
            {
                Title = "Harry Potter",
                Name = "Pippo",
                Surname = "Franco",
                PublishingHouse = "Salani",
            };
            DataTableAccess<Book> da = new();
            BookHandler handler = new(da);
            Assert.IsTrue(handler.AddMany(book, 15));
            handler.Save();

        }

        [TestMethod()]
        public void AddManyOfNew()
        {
            Book book = new Book()
            {
                Title = "Harry Porker",
                Name = "Pippo2",
                Surname = "Franco2",
                PublishingHouse = "Salani2",
            };
            DataTableAccess<Book> da = new();
            BookHandler handler = new(da);
            Assert.IsTrue(handler.AddMany(book, 7));
            handler.Save();
        }
    }
}
