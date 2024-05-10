using BusinessLogic.Library.Authentication;

namespace ConsoleApp.Library
{
    internal class Application : IRunnable
    {
        private Session _session;

        public Application()
        {
            _session = Session.GetInstance();
        }

        public void Run()
        {
            throw new NotImplementedException();
        }

        public void Close()
        {
            _session.Logout();
            Environment.Exit(0);
        }
    }
}
