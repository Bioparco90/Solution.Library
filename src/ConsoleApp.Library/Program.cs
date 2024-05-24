using BusinessLogic.Library;
using BusinessLogic.Library.Authentication;
using BusinessLogic.Library.Interfaces;
using ConsoleApp.Library;
using ConsoleApp.Library.Views;
using DataAccessLayer.Library.DAO;
using DataAccessLayer.Library.Repository;
using DataAccessLayer.Library.Repository.Interfaces;

Session session = Session.GetInstance();

DatabaseContext db = new();
UserDAO userDAO = new(db);
BookDAO bookDao = new(db);
ReservationDAO reservationDAO = new(db);

IUserRepository userRepository = new UserRepository(userDAO);
IBookRepository bookRepository = new BookRepository(bookDao);
IReservationRepository reservationRepository = new ReservationRepository(reservationDAO);

IUserHandler userHandler = new UserHandler(userRepository);
IReservationHandler reservationHandler = new ReservationHandler(session, reservationRepository);
IBookHandler bookHandler = new BookHandler(session, bookRepository, reservationHandler);

Utils utils = new();
AdminView adminView = new(session, utils, userHandler, bookHandler, reservationHandler);
LoginView loginView = new(utils);
Menu menu = new(utils, loginView, adminView);

Application app = new(menu);

app.Run();
