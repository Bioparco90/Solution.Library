namespace ConsoleApp.Library
{
    internal class Menu
    {
        public MenuUtils Utils;
        private LoginView _loginView;

        public Menu(MenuUtils utils, LoginView loginView)
        {
            Utils = utils;
            _loginView = loginView;
        }

        private bool CheckLoginInput(string input) => input != "1" && input != "2" || input == string.Empty;

        public bool StartMenu()
        {
            string choice = Utils.GetStrictInteraction("1. Login\n2. Exit", CheckLoginInput);
            return choice == "1";
        }

        public LoginRecord LoginMenu() => _loginView.AskLogin();
    }
}
