namespace ConsoleApp.Library.Views.Interfaces
{
    internal interface IAdminView : IView
    {
        bool AddBook();
        bool DeleteBook();
        bool UpdateBook();
    }
}