namespace ConsoleApp.Library.Views
{
    internal partial class AdminView
    {
        public override void HomeMenu()
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
    }
}
