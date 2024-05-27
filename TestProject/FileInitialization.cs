using BusinessLogic.Library.V1;
using DataAccessLayer.Library;
using Model.Library;
using Model.Library.Enums;

namespace FileInitializations
{
    [TestClass]
    public class FileInitialization
    {
        [TestMethod()]
        public void CreateUsers()
        {
            User user1 = new User()
            {
                Username = "admin1",
                Password = "pippo",
                Role = Role.Admin,
            };

            User user2 = new User()
            {
                Username = "user1",
                Password = "franco",
                Role = Role.User,
            };
            DataTableAccess<User> da = new();
            UserHandler handler = new(da);

            Assert.IsTrue(handler.Add(user1));
            Assert.IsTrue(handler.Add(user2));
            if (!File.Exists(da.XMLFileName))
            {
                handler.Save();
            }
        }

        [TestMethod()]
        public void CreateReservations()
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
            reservations.ForEach(r => Assert.IsTrue(handler.Add(r)));
            if (!File.Exists(da.XMLFileName))
            {
                handler.Save();
            }
        }

        [TestMethod()]
        public void CreateBooks()
        {
            List<Book> books = new List<Book>
        {
            new Book
            {
                Title = "Il signore degli anelli",
                AuthorName = "J.R.R.",
                AuthorSurname = "Tolkien",
                PublishingHouse = "Mondadori",
                Quantity = 10
            },
            new Book
            {
                Title = "1984",
                AuthorName = "George",
                AuthorSurname = "Orwell",
                PublishingHouse = "Mondadori",
                Quantity = 8
            },
            new Book
            {
                Title = "Orgoglio e pregiudizio",
                AuthorName = "Jane",
                AuthorSurname = "Austen",
                PublishingHouse = "Mondadori",
                Quantity = 5
            },
            new Book
            {
                Title = "Cronache del ghiaccio e del fuoco",
                AuthorName = "George R.R.",
                AuthorSurname = "Martin",
                PublishingHouse = "Mondadori",
                Quantity = 7
            },
            new Book
            {
                Title = "Harry Potter e la pietra filosofale",
                AuthorName = "J.K.",
                AuthorSurname = "Rowling",
                PublishingHouse = "Salani",
                Quantity = 12
            },
            new Book
            {
                Title = "Harry Potter e la camera dei segreti",
                AuthorName = "J.K.",
                AuthorSurname = "Rowling",
                PublishingHouse = "Salani",
                Quantity = 10
            }
        };
            DataTableAccess<Book> da = new();
            BookHandler handler = new(da);
            books.ForEach(b => Assert.IsTrue(handler.Upsert(b, b.Quantity)));
            if (!File.Exists(da.XMLFileName))
            {
                handler.Save();
            }
        }
    }
}
