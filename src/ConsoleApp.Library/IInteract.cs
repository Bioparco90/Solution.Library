namespace ConsoleApp.Library
{
    internal interface IInteract
    {
        public string GetStrictInteraction(string message, Func<string, bool> constraint, Func<string>? readPassword = null);
        public string GetInteraction(string message, Func<string>? readPassword = null);
    }
}