namespace ConsoleApp.Library
{
    internal class MenuUtils : IInteract
    {
        public string GetStrictInteraction(string message, Func<string, bool> constraint)
        {
            string input;
            do
            {
                input = GetInteraction(message);
                if (input == string.Empty)
                {
                    Console.WriteLine("Invalid command.");
                }
            } while (constraint(input));

            return input;
        }

        public string GetInteraction(string message)
        {
            Console.Write($"{message}: ");
            return Console.ReadLine()?.Trim() ?? string.Empty;
        }
    }
}
