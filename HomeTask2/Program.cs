using Microsoft.EntityFrameworkCore;
using HomeTask2;
using DataLayer.Context;
using DataLayer.Repository;
using DataLayer.Models;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

string connection = builder.Configuration.GetConnectionString("DefaultConnection");
//builder.Services.AddDbContext<BookstoreContext>(options => options.UseSqlServer(connection));

//builder.Services.AddScoped<IRepository<Book>, BookRepository>();
var app = builder.Build();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action}/{order}");

app.Run();

