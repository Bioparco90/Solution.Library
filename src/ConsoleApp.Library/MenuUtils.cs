namespace ConsoleApp.Library
{
    internal class MenuUtils : IInteract
    {
        public string GetStrictInteraction(string message, Func<string, bool> constraint)
        {
            string input;
            bool isInvalid;
            do
            {
                input = GetInteraction(message);
                isInvalid = constraint(input);
                if (isInvalid)
                {
                    Console.WriteLine("Invalid input.");
                }
            } while (isInvalid);

            return input;
        }

        public string GetInteraction(string message)
        {
            Console.Write($"{message}: ");
            return Console.ReadLine()?.Trim() ?? string.Empty;
        }
    }
}
