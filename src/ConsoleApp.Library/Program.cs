using ConsoleApp.Library;

Console.WriteLine("Don't delete me");

MenuUtils menuUtils = new();
LoginView loginView = new(menuUtils);

Application app = new Application();

app.Run();
