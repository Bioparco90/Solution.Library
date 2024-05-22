using BusinessLogic.Library.Authentication;
using BusinessLogic.Library.Exceptions;
using BusinessLogic.Library.Interfaces;
using DataAccessLayer.Library.Repository;
using Model.Library;

namespace BusinessLogic.Library.Tests
{
    [TestClass()]
    public class BookHandlerTests
    {
        [TestMethod()]
        public void UpsertTestNoLogin()
        {
            Assert.ThrowsException<SessionNotStartedException>(() =>
            {
                Book book = new()
                {
                    Title = "Test",
                    AuthorName = "Test",
                    AuthorSurname = "Test",
                    PublishingHouse = "Test",
                    Quantity = 7
                };

                Session session = Session.GetInstance();
                IBookHandler bh = new BookHandler(session, new BookRepository(new(new())));
                bh.Upsert(book);
            });
        }

        [TestMethod()]
        public void UpsertTestUnauthorized()
        {
            Assert.ThrowsException<UnauthorizedUserException>(() =>
            {
                Book book = new()
                {
                    Title = "Test",
                    AuthorName = "Test",
                    AuthorSurname = "Test",
                    PublishingHouse = "Test",
                    Quantity = 7
                };

                Session session = Session.GetInstance();
                session.Login("user1", "pippo");
                IBookHandler bh = new BookHandler(session, new BookRepository(new(new())));
                bh.Upsert(book);
            });
        }

        [TestMethod()]
        public void UpsertTestNullSession()
        {
            Assert.ThrowsException<NullReferenceException>(() =>
            {
                Book book = new()
                {
                    Title = "Test",
                    AuthorName = "Test",
                    AuthorSurname = "Test",
                    PublishingHouse = "Test",
                    Quantity = 7
                };

                Session session = null;
                IBookHandler bh = new BookHandler(session, new BookRepository(new(new())));
                bh.Upsert(book);
            });
        }

        [TestMethod()]
        public void UpsertTestNew()
        {
            Book book = new()
            {
                Title = "Test",
                AuthorName = "Test",
                AuthorSurname = "Test",
                PublishingHouse = "Test",
                Quantity = 7
            };

            Session session = Session.GetInstance();
            session.Login("admin", "12345");
            IBookHandler bh = new BookHandler(session, new BookRepository(new(new())));
            Assert.IsTrue(bh.Upsert(book));
        }

        [TestMethod()]
        public void UpsertTestExisting()
        {
            Book book = new()
            {
                Title = "Origin",
                AuthorName = "Dan",
                AuthorSurname = "Brown",
                PublishingHouse = "Mondadori",
                Quantity = 7
            };

            Session session = Session.GetInstance();
            session.Login("admin", "12345");
            IBookHandler bh = new BookHandler(session, new BookRepository(new(new())));
            Assert.IsTrue(bh.Upsert(book));
        }

        [TestMethod()]
        public void UpsertTestExistingLowerCase()
        {
            Book book = new()
            {
                Title = "origin",
                AuthorName = "dan",
                AuthorSurname = "brown",
                PublishingHouse = "mondadori",
                Quantity = 7
            };

            Session session = Session.GetInstance();
            session.Login("admin", "12345");
            IBookHandler bh = new BookHandler(session, new BookRepository(new(new())));
            Assert.IsTrue(bh.Upsert(book));
        }

        [TestMethod()]
        public void UpdateTest()
        {
            var newBook = new Book()
            {
                Title = "paperinik",
                AuthorName = "paolino paperino",
                AuthorSurname = "donald duck",
                PublishingHouse = "Disney"
            };
            newBook.Id = Guid.Parse("9721e41a-3d40-4d3d-a7f6-02f9611ae5bd");
            newBook.Quantity = 17;

            Session session = Session.GetInstance();
            session.Login("admin", "12345");
            IBookHandler bh = new BookHandler(session, new BookRepository(new(new())));

            var result = bh.Update(newBook);
            Assert.IsTrue(result);
        }
    }
}
