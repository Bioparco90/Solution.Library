using BusinessLogic.Library.Enums;
using Model.Library;

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

        public bool SearchBooks(out List<Book> books)
        {
            var book = BuildBook(Method.Get);
            books = _bookHandler.SearchMany(book).ToList();
            return books.Count > 0;
        }

        public bool LoanBook()
        {
            Console.WriteLine("All the following fields are mandatory");

            var book = BuildBook(Method.Loan);
            return _bookHandler.Loan(book);
        }

        public bool GiveBackBook()
        {
            var book = BuildBook(Method.EndLoan);
            return _bookHandler.GiveBackBook(book);
        }
    }
}
