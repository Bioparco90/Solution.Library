using DataAccessLayer.Library;
using Model.Library;
using System.Data;

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

        //[TestMethod()]
        //public void DeleteBookFail()
        //{
        //    Book book = new Book()
        //    {
        //        Title = "Harry Potter",
        //        AuthorName = "J.K.",
        //        AuthorSurname = "Rowling",
        //        PublishingHouse = "Salani"
        //    };

        //    DataTableAccess<Book> da = new();
        //    BookHandler handler = new(da);
        //    Assert.IsFalse(handler.Delete(book));
        //}

        //[TestMethod()]
        //public void DeleteBookSuccess()
        //{
        //    Book book = new Book()
        //    {
        //        Title = "Harry Potter e la pietra filosofale",
        //        AuthorName = "J.K.",
        //        AuthorSurname = "Rowling",
        //        PublishingHouse = "Salani"
        //    };

        //    DataTableAccess<Book> da = new();
        //    BookHandler handler = new(da);
        //    Assert.IsTrue(handler.Delete(book));
        //}

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
            Assert.IsTrue(handler.Upsert(book, 15));
        }

        [TestMethod()]
        public void AddManyOfNew()
        {
            Book book = new()
            {
                Title = "TestCreate",
                AuthorName = "Er",
                AuthorSurname = "Brasiliano",
                PublishingHouse = "Onlyfans"
            };
            DataTableAccess<Book> da = new();
            BookHandler handler = new(da);
            Assert.IsTrue(handler.Upsert(book, 2));
            handler.Save();
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

        [TestMethod()]
        public void ConstraintViolation()
        {
            DataTableAccess<Book> da = new();
            DataTable dt = da.ReadDataTableFromFile(da.XMLFileName);

            Book b1 = new()
            {
                Id = Guid.NewGuid(),
                Title = "Harry Potter e la pietra filosofale",
                AuthorName = "J.K.",
                AuthorSurname = "Rowling",
                PublishingHouse = "Salani"
            };

            try
            {
                da.AddItemToDataTable(b1, dt);
                dt.WriteXml(da.XMLFileName, XmlWriteMode.WriteSchema);
                Assert.Fail("Exception not thrown");
            }
            catch
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod()]
        public void ConstraintNotViolation()
        {
            DataTableAccess<Book> da = new();
            DataTable dt = da.ReadDataTableFromFile(da.XMLFileName);

            Book b1 = new()
            {
                Id = Guid.NewGuid(),
                Title = "Harry Potter e il calice di fuoco",
                AuthorName = "J.K.",
                AuthorSurname = "Rowling",
                PublishingHouse = "Salani"
            };

            try
            {
                da.AddItemToDataTable(b1, dt);
                dt.WriteXml(da.XMLFileName, XmlWriteMode.WriteSchema);
                Assert.IsTrue(true);
            }
            catch(Exception ex)
            {
                Assert.Fail($"Exception thrown: {ex.Message}");
            }
        }
    }
}
