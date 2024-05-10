using ConsoleApp.Library;

MenuUtils menuUtils = new();
LoginView loginView = new(menuUtils);

Application app = new Application();

app.Run();
