
using DataLayer.Models;
using DataLayer.Repository;
using HomeTask2;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
var webApiOptions = new WebApiOptions();
builder.Services.Configure<WebApiOptions>(builder.Configuration.GetSection(/*"ApiKey"*/WebApiOptions.WebApi));

//string connection = builder.Configuration.GetConnectionString("DefaultConnection");
//builder.Services.AddDbContext<BookstoreContext>(options => options.UseSqlServer(connection));

builder.Services.AddScoped<IRepository<Book>, BookRepository>();
builder.Services.AddScoped<IRepository<Review>, ReviewRepository>();
builder.Services.AddScoped<IRepository<Rating>, RatingRepository>();

var app = builder.Build();

//app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action}/{id?}");

app.Run();

