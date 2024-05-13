namespace ConsoleApp.Library
{
    internal class Utils : IInteract, ICheck, IHide
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

        public bool CheckEmpty(string input)
        {
            return input == string.Empty;
        }

        public bool CheckInputChoice(string input, int numberOfChoices) =>
            !Enumerable.Range(1, numberOfChoices)
                .Select(x => x.ToString())
                .Contains(input)
            || input == string.Empty;

        public string HidePassword()
        {
            string password = "";
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true); // true per non mostrare il tasto premuto
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    password += key.KeyChar;
                    Console.Write("*"); // Visualizza un carattere di oscuramento
                }
                else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password.Remove(password.Length - 1);
                    Console.Write("\b \b"); // Cancella l'ultimo carattere visualizzato
                }
            } while (key.Key != ConsoleKey.Enter);

            Console.WriteLine();
            return password;
        }
    }
}
