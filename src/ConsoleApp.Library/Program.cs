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

Session session = Session.GetInstance();

IOpenConnection db = new DatabaseContext();
IUserDAO userDAO = new UserDAO(db);
IBookDAO bookDao = new BookDAO(db);
IReservationDAO reservationDAO = new ReservationDAO(db);

IUserRepository userRepository = new UserRepository(userDAO);
IBookRepository bookRepository = new BookRepository(bookDao);
IReservationRepository reservationRepository = new ReservationRepository(reservationDAO);

IUserHandler userHandler = new UserHandler(userRepository);
IReservationHandler reservationHandler = new ReservationHandler(session, reservationRepository);
IBookHandler bookHandler = new BookHandler(session, bookRepository, reservationHandler);

Utils utils = new();
AdminView adminView = new(session, utils, userHandler, bookHandler, reservationHandler);
UserView userView = new(session, utils, reservationHandler, bookHandler);
ILoginView loginView = new LoginView(utils);
IMenu menu = new Menu(session, utils, loginView, adminView, userView);

Application app = new(menu);

app.Run();
