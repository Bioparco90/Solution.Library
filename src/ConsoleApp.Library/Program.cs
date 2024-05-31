using BusinessLogic.Library;
using BusinessLogic.Library.Authentication;
using BusinessLogic.Library.Interfaces;
using ConsoleApp.Library;
using ConsoleApp.Library.Interfaces;
using ConsoleApp.Library.Views;
using ConsoleApp.Library.Views.Interfaces;
using DataAccessLayer.Library.DAO;
using DataAccessLayer.Library.DAO.Interfaces;
using DataAccessLayer.Library.Repository;
using DataAccessLayer.Library.Repository.Interfaces;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = new ServiceCollection()
    .AddSingleton<Session>(sp => Session.GetInstance())
    .AddTransient<IOpenConnection, DatabaseContext>()
    .AddTransient<IUserDAO, UserDAO>()
    .AddTransient<IBookDAO, BookDAO>()
    .AddTransient<IReservationDAO, ReservationDAO>()
    .AddTransient<IUserRepository, UserRepository>()
    .AddTransient<IBookRepository, BookRepository>()
    .AddTransient<IReservationRepository, ReservationRepository>()
    .AddTransient<IUserHandler, UserHandler>()
    .AddTransient<IReservationHandler, ReservationHandler>()
    .AddTransient<IBookHandler, BookHandler>()
    .AddTransient<IAdminView, AdminView>()
    .AddTransient<IUserView, UserView>()
    .AddTransient<ILoginView, LoginView>()
    .AddTransient<IMenu, Menu>()
    .AddTransient<Utils>()
    .AddTransient<Application>()
    .BuildServiceProvider();

var app = serviceProvider.GetService<Application>();
app?.Run();
