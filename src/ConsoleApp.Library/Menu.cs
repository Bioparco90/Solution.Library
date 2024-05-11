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

        public static bool CheckInputChoice(string input, int numberOfChoices) => 
            !Enumerable.Range(1, numberOfChoices)
                        .Select(x => x.ToString())
                        .Contains(input)
            || input == string.Empty;

        public bool StartMenu()
        {
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Exit");

            string choice = Utils.GetStrictInteraction("Insert command", input => CheckInputChoice(input, 2));
            return choice == "1";
        }

        public LoginRecord LoginMenu() => _loginView.AskLogin();
    }
}
