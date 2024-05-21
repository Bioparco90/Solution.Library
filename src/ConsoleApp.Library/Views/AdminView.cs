using Model.Library;
using BusinessLogic.Library.Interfaces;

namespace ConsoleApp.Library.Views
{
    internal class AdminView
    {
        private readonly Utils _utils;
        private readonly IBookHandler _bookHandler;

        public AdminView(Utils utils, IBookHandler bookHandler)
        {
            _utils = utils;
            _bookHandler = bookHandler;
        }

        public void HomeMenu()
        {
            Console.WriteLine("1. Add new book");
            Console.WriteLine("2. Update book");
            Console.WriteLine("3. Delete book");
            Console.WriteLine("4. Search book");
            Console.WriteLine("5. Loan book");
            Console.WriteLine("6. Give back book");
            Console.WriteLine("7. Reservations history");
            Console.WriteLine("8. Exit");
        }

        public bool AddBook()
        {
            var book = BuildBook();
            return _bookHandler.Upsert(book);
        }

        private Book BuildBook()
        {
            Console.WriteLine("All the following fields are mandatory");
            string title = _utils.GetStrictInteraction("Title", _utils.CheckEmpty);
            string authorName = _utils.GetStrictInteraction("Author Name", _utils.CheckEmpty);
            string authorSurname = _utils.GetStrictInteraction("Author Surname", _utils.CheckEmpty);
            string publishingHouse = _utils.GetStrictInteraction("Publishing House", _utils.CheckEmpty);
            int quantity = _utils.GetStrictInteraction("Quantity");

            return new()
            {
                Title = title,
                AuthorName = authorName,
                AuthorSurname = authorSurname,
                PublishingHouse = publishingHouse,
                Quantity = quantity
            };
        }
    }
}
