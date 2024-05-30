namespace ConsoleApp.Library.Views.Interfaces
{
    internal interface ILoginView
    {
        LoginRecord AskLogin();
        string AskRetry();
    }
}