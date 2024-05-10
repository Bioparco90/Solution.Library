namespace ConsoleApp.Library
{
    internal class MenuUtils
    {
        public string GetStrictInteraction(string inputRequested)
        {
            string input;
            do
            {
               input = GetInteraction(inputRequested);
            } while (input == string.Empty);

            return input;
        }

        public string GetInteraction(string inputRequested)
        {
            Console.Write($"{inputRequested}: ");
            return Console.ReadLine()?.Trim() ?? string.Empty;
        }
    }
}
