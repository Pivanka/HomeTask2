
using DataLayer.Models;
using DataLayer.Repository;
using FluentValidation;
using HomeTask2;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
var webApiOptions = new WebApiOptions();
builder.Services.Configure<WebApiOptions>(builder.Configuration.GetSection(/*"ApiKey"*/WebApiOptions.WebApi));

//string connection = builder.Configuration.GetConnectionString("DefaultConnection");
//builder.Services.AddDbContext<BookstoreContext>(options => options.UseSqlServer(connection));

builder.Services.AddScoped<IRepository<Book>, BookRepository>();
builder.Services.AddScoped<IRepository<Review>, ReviewRepository>();
builder.Services.AddScoped<IRepository<Rating>, RatingRepository>();
//builder.Services.AddScoped<IValidator<Rating>, RatingValidator>();

builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseDeveloperExceptionPage();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action}/{id?}");

app.Run();

