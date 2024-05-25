namespace ConsoleApp.Library.Interfaces
{
    internal interface IInteract
    {
        public string GetStrictInteraction(string message, Func<string, bool> constraint);
        public string GetStrictInteraction(string message, Func<string, bool> constraint, Func<string> readPassword);
        public int GetStrictInteraction(string message);
        public string GetInteraction(string message);
        public string GetInteraction(string message, Func<string> readPassword);
    }
}