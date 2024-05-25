using BusinessLogic.Library.Enums;

namespace ConsoleApp.Library.Views
{
    internal partial class AdminView
    {
        public bool AddBook()
        {
            Console.WriteLine("All the following fields are mandatory");
            var book = BuildBook(Method.Add);
            return _bookHandler.Upsert(book);
        }

        public bool UpdateBook()
        {
            Console.WriteLine("All the following fields are mandatory");
            var book = BuildBook(Method.Update);
            var found = _bookHandler.SearchSingle(book, parametersCount => parametersCount == 4);

            Console.WriteLine("All the following fields are mandatory");
            var newBook = BuildBook(Method.Update);
            newBook.Id = found.Id;
            newBook.Quantity = found.Quantity;

            return _bookHandler.Update(newBook);
        }

        public bool DeleteBook()
        {
            Console.WriteLine("All the following fields are mandatory");
            var book = BuildBook(Method.Delete);
            return _bookHandler.Delete(book);
        }
    }
}
