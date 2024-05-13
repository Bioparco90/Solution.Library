namespace ConsoleApp.Library
{
    internal interface ICheck
    {
        public bool CheckEmpty(string input);
        public bool CheckInputChoice(string input, int numberOfChoices);
    }
}
