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
                if (constraint(input))
                {
                    Console.WriteLine("Invalid input.");
                }
            } while (constraint(input));

            return input;
        }

        public string GetInteraction(string message)
        {
            Console.WriteLine($"{message}");
            return Console.ReadLine()?.Trim() ?? string.Empty;
        }
    }
}
