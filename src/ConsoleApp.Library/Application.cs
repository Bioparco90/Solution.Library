using BusinessLogic.Library.Authentication;
using ConsoleApp.Library.Interfaces;

namespace ConsoleApp.Library
{
    internal class Application : IRunnable
    {
        private readonly Session _session;
        private readonly IMenu _menu;

        public Application(IMenu menu)
        {
            _session = Session.GetInstance();
            _menu = menu;
        }

        public void Run()
        {
            if (!_menu.StartMenu())
            {
                Close();
            }

            _menu.LoginMenu();

            if (_session.IsAdmin)
            {
                _menu.AdminMenu();
            }
            else
            {
                _menu.UserMenu();
            }
        }

        public static void Close() => Environment.Exit(0);
    }
}
