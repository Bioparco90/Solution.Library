using BusinessLogic.Library.Exceptions;
using System.Data.SqlClient;

namespace ConsoleApp.Library.Views
{
    internal partial class AdminView
    {
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
            catch (BookOnLoanException ex) when (ex.ActiveReservations is not null)
            {
                ShowBooksOnLoan(ex.ActiveReservations!);
            }
            catch (CustomException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (SqlException ex)
            {
                Console.WriteLine("An error occurred in database:\n" + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected Error occurred:\n" + ex.Message);
            }
        }
    }
}
