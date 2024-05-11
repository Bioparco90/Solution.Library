using ConsoleApp.Library;

MenuUtils menuUtils = new();
LoginView loginView = new(menuUtils);
Menu menu = new(menuUtils, loginView);

Application app = new(menu, menuUtils);

app.Run();
