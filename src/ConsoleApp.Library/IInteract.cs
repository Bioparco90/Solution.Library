namespace ConsoleApp.Library
{
    internal interface IInteract
    {
        public string GetStrictInteraction(string message, Func<string, bool> constraint);
        public string GetInteraction(string message);
    }
}