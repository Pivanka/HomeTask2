using DataLayer.Models;
using DataLayer.Repository;
using HomeTask2;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
var webApiOptions = new WebApiOptions();
builder.Services.Configure<WebApiOptions>(builder.Configuration.GetSection(WebApiOptions.WebApi));


builder.Services.AddScoped<IRepository<Book>, BookRepository>();
builder.Services.AddScoped<IRepository<Review>, ReviewRepository>();
builder.Services.AddScoped<IRepository<Rating>, RatingRepository>();

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

