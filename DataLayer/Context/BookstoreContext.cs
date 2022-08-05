using DataLayer.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Context
{
    public class BookstoreContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Review> Reviews { get; set; }

        public DbSet<Rating> Ratings { get; set; }
        public BookstoreContext()
        {
            this.Database.EnsureDeleted();
            this.Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionalBiulder)
        {
            var connectionStringBuilder = new SqlConnectionStringBuilder
            {
                ["Server"] = "DESKTOP-P7QOF3N",
                ["DataBase"] = "ExampleDb",
                ["Trusted_Connection"] = true
            };

            Console.WriteLine(connectionStringBuilder.ConnectionString);

            optionalBiulder
                .UseSqlServer(connectionStringBuilder.ConnectionString);
            //"Server=DESKTOP-P7QOF3N;Database=ExampleDb;Trusted_Connection=True;");
        }
    }
}