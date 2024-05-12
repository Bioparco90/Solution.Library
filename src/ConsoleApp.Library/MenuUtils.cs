namespace ConsoleApp.Library
{
    internal class MenuUtils : IInteract
    {
        public string GetStrictInteraction(string message, Func<string, bool> constraint, Func<string>? readPassword = null)
        {
            string input;
            bool isInvalid;
            do
            {
                input = readPassword is null
                    ? GetInteraction(message)
                    : GetInteraction(message, readPassword);

                isInvalid = constraint(input);
                if (isInvalid)
                {
                    Console.WriteLine("Invalid input.");
                }
            } while (isInvalid);

            return input;
        }

        public string GetInteraction(string message, Func<string>? readPassword = null)
        {
            if (readPassword is not null)
            {
                Console.Write($"{message}: ");
                return readPassword();
            }

            Console.Write($"{message}: ");
            return Console.ReadLine()?.Trim() ?? string.Empty;
        }
    }
}
