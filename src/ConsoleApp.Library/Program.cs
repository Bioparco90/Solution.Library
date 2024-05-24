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
BookDAO bookDao = new(db);
ReservationDAO reservationDAO = new(db);

IBookRepository bookRepository = new BookRepository(bookDao);
IReservationRepository reservationRepository = new ReservationRepository(reservationDAO);

IReservationHandler reservationHandler = new ReservationHandler(session, reservationRepository);
IBookHandler bookHandler = new BookHandler(session, bookRepository, reservationHandler);

Utils utils = new();
AdminView adminView = new(session, utils, bookHandler, reservationHandler);
LoginView loginView = new(utils);
Menu menu = new(utils, loginView, adminView);

Application app = new(menu);

app.Run();
