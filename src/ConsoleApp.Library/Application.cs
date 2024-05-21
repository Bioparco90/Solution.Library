using BusinessLogic.Library.Authentication;
using ConsoleApp.Library.Interfaces;

namespace ConsoleApp.Library
{
    internal class Application : IRunnable
    {
        private Session _session;
        private readonly Menu _menu;

        public Application(Menu menu)
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

            _menu.LoginMenu(_session);

            if (_session.IsAdmin)
            {
                Console.WriteLine("Admin menu");
            }
            else
            {
                Console.WriteLine("Basic user menu");
            }
        }

        public static void Close() => Environment.Exit(0);
    }
}
