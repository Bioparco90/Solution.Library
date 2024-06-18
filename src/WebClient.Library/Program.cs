using BusinessLogic.Library;
using BusinessLogic.Library.Authentication;
using BusinessLogic.Library.Interfaces;

using DataAccessLayer.Library.DAO;
using DataAccessLayer.Library.DAO.Interfaces;
using DataAccessLayer.Library.Repository;
using DataAccessLayer.Library.Repository.Interfaces;
using WebClient.Library.ApplicationState;
using WebClient.Library.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddSingleton(sp => Session.GetInstance());
builder.Services.AddTransient<IOpenConnection, DatabaseContext>();
builder.Services.AddTransient<IUserDAO, UserDAO>();
builder.Services.AddTransient<IBookDAO, BookDAO>();
builder.Services.AddTransient<IReservationDAO, ReservationDAO>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IBookRepository, BookRepository>();
builder.Services.AddTransient<IReservationRepository, ReservationRepository>();
builder.Services.AddTransient<IUserHandler, UserHandler>();
builder.Services.AddTransient<IReservationHandler, ReservationHandler>();
builder.Services.AddTransient<IBookHandler, BookHandler>();

builder.Services.AddScoped<AppStateManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();