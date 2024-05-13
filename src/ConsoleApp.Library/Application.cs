using BusinessLogic.Library.Authentication;

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

            DoLogin();

            if (_session.IsAdmin)
            {
                Console.WriteLine("Admin menu");
            }
            else
            {
                Console.WriteLine("Basic user menu");
            }
        }

        private void DoLogin() => _menu.LoginMenu(_session);

        public static void Close()
        {
            Environment.Exit(0);
        }
    }
}
