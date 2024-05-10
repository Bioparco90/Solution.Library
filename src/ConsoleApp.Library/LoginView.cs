namespace ConsoleApp.Library
{
    internal class LoginView
    {
        private readonly MenuUtils _menuUtils;

        public LoginView(MenuUtils menuUtils)
        {
            _menuUtils = menuUtils;
        }

        private bool CheckEmpty(string input)
        {
            return input == string.Empty;
        }

        public LoginRecord AskLogin()
        {
            string username = _menuUtils.GetStrictInteraction("Username", CheckEmpty);
            string password = _menuUtils.GetStrictInteraction("Password", CheckEmpty);
            
            return new(username, password);
        }
    }
}
