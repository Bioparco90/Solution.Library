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
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Exit");

            string choice = Utils.GetStrictInteraction("Insert command", CheckLoginInput);
            return choice == "1";
        }

        public LoginRecord LoginMenu() => _loginView.AskLogin();
    }
}
