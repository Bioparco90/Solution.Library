using Model.Library;
using BusinessLogic.Library.Interfaces;
using BusinessLogic.Library.Enums;

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

        public void View(Func<bool> action, string successInfo)
        {
            try
            {
                string message = action() ? successInfo : "Something went wrong";
                Console.WriteLine(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public bool AddBook()
        {
            var book = BuildBook(Method.None);
            return _bookHandler.Upsert(book);
        }

        public bool UpdateBook()
        {
            var book = BuildBook(Method.Update);
            var found = _bookHandler.SearchSingle(book, parametersCount => parametersCount == 4);
            if (found is null)
            {
                return false;
            }

            var newBook = BuildBook(Method.Update);
            newBook.Id = found.Id;
            newBook.Quantity = found.Quantity;

            return _bookHandler.Update(newBook);
        }

        private Book BuildBook(Method method)
        {
            Console.WriteLine("All the following fields are mandatory");
            string title, authorName, authorSurname, publishingHouse;
            int quantity = 0;

            switch (method)
            {
                case Method.Update:
                    AskAnagraphic(out title, out authorName, out authorSurname, out publishingHouse);
                    break;

                default:
                    AskAnagraphic(out title, out authorName, out authorSurname, out publishingHouse);
                    quantity = _utils.GetStrictInteraction("Quantity");
                    break;
            }

            return new()
            {
                Title = title,
                AuthorName = authorName,
                AuthorSurname = authorSurname,
                PublishingHouse = publishingHouse,
                Quantity = quantity
            };
        }

        private void AskAnagraphic(out string title, out string authorName, out string authorSurname, out string publishingHouse)
        {
            title = _utils.GetStrictInteraction("Title", _utils.CheckEmpty);
            authorName = _utils.GetStrictInteraction("Author Name", _utils.CheckEmpty);
            authorSurname = _utils.GetStrictInteraction("Author Surname", _utils.CheckEmpty);
            publishingHouse = _utils.GetStrictInteraction("Publishing House", _utils.CheckEmpty);
        }
    }
}
