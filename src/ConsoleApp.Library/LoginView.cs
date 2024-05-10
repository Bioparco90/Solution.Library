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
            string username = _menuUtils.GetStrictInteraction("Username");
            string password = _menuUtils.GetStrictInteraction("Password");
            
            return new(username, password);
        }
    }
}
