using ConsoleApp.Library.Views.Interfaces;

namespace ConsoleApp.Library.Views
{
    internal class LoginView : ILoginView
    {
        private readonly Utils _utils;

        public LoginView(Utils utils)
        {
            _utils = utils;
        }

        public LoginRecord AskLogin()
        {
            string username = _utils.GetStrictInteraction("Username", _utils.CheckEmpty);
            string password = _utils.GetStrictInteraction("Password", _utils.CheckEmpty, _utils.HidePassword);

            return new(username, password);
        }

        public string AskRetry()
        {
            Console.WriteLine("Invalid login. Retry?");
            Console.WriteLine("1. Yes");
            Console.WriteLine("2. No");

            return _utils.GetStrictInteraction("Insert command", input => _utils.CheckInputChoice(input, 2));
        }
    }
}
