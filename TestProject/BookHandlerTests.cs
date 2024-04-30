using DataAccessLayer.Library;
using Model.Library;

namespace BusinessLogic.Library.Tests
{
    [TestClass()]
    public class BookHandlerTests
    {
        [TestMethod()]
        public void GetByTitleTest()
        {
            DataTableAccess<Book> da = new();
            BookHandler handler = new(da);
            string title = "Harry Potter";
            var books = handler.GetByTitle(title).ToList();
            Assert.IsNotNull(books);
            Assert.IsTrue(books.Count == 2);
        }

        [TestMethod()]
        public void GetByAuthorNameTest()
        {
            DataTableAccess<Book> da = new();
            BookHandler handler = new(da);
            string name = "Jane";
            var books = handler.GetByAuthorName(name).ToList();
            Assert.IsNotNull(books);
            Assert.IsTrue(books.Count == 1);
        }

        [TestMethod()]
        public void DeleteBookFail()
        {
            Book book = new Book()
            {
                Title = "Harry Potter",
                AuthorName = "J.K.",
                AuthorSurname = "Rowling",
                PublishingHouse = "Salani"
            };

            DataTableAccess<Book> da = new();
            BookHandler handler = new(da);
            Assert.IsFalse(handler.Delete(book));
        }

        [TestMethod()]
        public void DeleteBookSuccess()
        {
            Book book = new Book()
            {
                Title = "Harry Potter e la pietra filosofale",
                AuthorName = "J.K.",
                AuthorSurname = "Rowling",
                PublishingHouse = "Salani"
            };

            DataTableAccess<Book> da = new();
            BookHandler handler = new(da);
            Assert.IsTrue(handler.Delete(book));
        }

        [TestMethod()]
        public void AddManyOfExisting()
        {
            Book book = new Book()
            {
                Title = "Harry Potter",
                AuthorName = "Pippo",
                AuthorSurname = "Franco",
                PublishingHouse = "Salani",
            };
            DataTableAccess<Book> da = new();
            BookHandler handler = new(da);
            Assert.IsTrue(handler.AddMany(book, 15));
        }

        [TestMethod()]
        public void AddManyOfNew()
        {
            Book book = new Book()
            {
                Title = "Harry Porker",
                AuthorName = "Pippo2",
                AuthorSurname = "Franco2",
                PublishingHouse = "Salani2",
            };
            DataTableAccess<Book> da = new();
            BookHandler handler = new(da);
            Assert.IsTrue(handler.AddMany(book, 7));
        }

        [TestMethod()]
        public void GetByAuthorSurnameTest()
        {
            DataTableAccess<Book> da = new();
            BookHandler handler = new(da);
            string surname = "rowling";
            var books = handler.GetByAuthorSurname(surname).ToList();
            Assert.IsNotNull(books);
            Assert.IsTrue(books.Count == 2);
        }

        [TestMethod()]
        public void GetByPublishingHouseTest()
        {
            DataTableAccess<Book> da = new();
            BookHandler handler = new(da);
            string publishingHouse = "salani";
            var books = handler.GetByPublishingHouse(publishingHouse).ToList();
            Assert.IsNotNull(books);
            Assert.IsTrue(books.Count == 2);
        }

        [TestMethod()]
        public void GetByQuantityTest()
        {
            DataTableAccess<Book> da = new();
            BookHandler handler = new(da);
            int quantity = 10;
            var books = handler.GetByQuantity(quantity).ToList();
            Assert.IsNotNull(books);
            Assert.IsTrue(books.Count == 2);
        }

        [TestMethod()]
        public void GetByPropertiesTest()
        {
            DataTableAccess<Book> da = new();
            BookHandler handler = new(da);

            SearchBooksParams parameters = new()
            {
                Title = "Harry Potter",
                AuthorSurname = "rowling"
            };

            var books = handler.GetByProperties(parameters)?.ToList();
            Assert.IsNotNull(books);
            Assert.IsTrue(books.Count == 2, $"Count = {books.Count}");
        }
    }
}
