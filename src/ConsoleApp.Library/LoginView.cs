namespace ConsoleApp.Library
{
    internal class LoginView
    {
        private readonly MenuUtils _menuUtils;

        public LoginView(MenuUtils menuUtils)
        {
            _menuUtils = menuUtils;
        }

        public LoginRecord AskLogin()
        {
            string username = _menuUtils.GetStrictInteraction("Username", (input) => input == string.Empty);
            string password = _menuUtils.GetStrictInteraction("Password", (input) => input == string.Empty);
            
            return new(username, password);
        }
    }
}
