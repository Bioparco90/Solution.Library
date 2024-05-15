using ConsoleApp.Library;

Utils menuUtils = new();
LoginView loginView = new(menuUtils);
Menu menu = new(menuUtils, loginView);

Application app = new(menu);

app.Run();
