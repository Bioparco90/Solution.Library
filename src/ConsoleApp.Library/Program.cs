using ConsoleApp.Library;
using ConsoleApp.Library.Views;

Utils menuUtils = new();
LoginView loginView = new(menuUtils);
Menu menu = new(menuUtils, loginView);

Application app = new(menu);

app.Run();
