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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Book[] books = new Book[]
                {
                    new Book { Title = "1984", Author = "Goerge Orwell", Cover="https://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1327144697l/3744438.jpg", Content="1984 is a dystopian novella by George Orwell published in 1949, which follows the life of Winston Smith, a low ranking member of 'the Party', who is frustrated by the omnipresent eyes of the party, and its ominous ruler Big Brother. 'Big Brother' controls every aspect of people's lives.", Genre="novel"},
                    new Book { Title = "The Little Prince", Author = "Antoine de Saint-Exupéry", Cover="https://images-na.ssl-images-amazon.com/images/I/71OZY035QKL.jpg", Content="The Little Prince is an honest and beautiful story about loneliness, friendship, sadness, and love. The prince is a small boy from a tiny planet (an asteroid to be precise), who travels the universe, planet-to-planet, seeking wisdom. On his journey, he discovers the unpredictable nature of adults.", Genre="novella"},
                    new Book { Title = "Harry Potter", Author = "J. K. Rowling", Cover="https://images.ctfassets.net/usf1vwtuqyxm/3d9kpFpwHyjACq8H3EU6ra/85673f9e660407e5e4481b1825968043/English_Harry_Potter_4_Epub_9781781105672.jpg?w=914&q=70&fm=jpg", Content="The novels chronicle the lives of a young wizard, Harry Potter, and his friends Hermione Granger and Ron Weasley, all of whom are students at Hogwarts School of Witchcraft and Wizardry. The main story arc concerns Harry's struggle against Lord Voldemort, a dark wizard who intends to become immortal, overthrow the wizard governing body known as the Ministry of Magic and subjugate all wizards and Muggles (non-magical people).", Genre="fantasy novel"}
                };
            modelBuilder.Entity<Book>().HasData(books);

            modelBuilder.Entity<Review>().HasData(
                new Review[]
                {
                    new Review { Book = books[0], Message="This novel is another dystopia by George Orwell. I read the book after Orwell's \"Collective Animal Farm\". In this and that book, you can contemplate the terrible model of the Soviet Union or other totalitarian states. However, in the novel \"1984\" it is depicted much more deeply. Therefore, sometimes when you read, you cannot believe that humanity has really lost such a model of behavior and only a few are able to fight against it. However, will this lead to victory and the overthrow of the system? After reading this work, I can only say that it is always worth fighting against what does not reach you. Do not be silent. After all, there can be thousands or millions of people like you, and together you can achieve much more than you can alone. Definitely recommend reading!", Reviewer="Ksyusha"}
                    new Review { Book = books[2], Message="Harry Potter is not just a story about a boy who lost his parents early. This series of books is about a great magician who went through a thorny path from an ordinary child who lived in a pantry under the stairs to a man with a capital letter, whose fate and exploits everyone knew, respected, admired.", Reviewer="John"}
                    new Review {Book = books[1], Message="A beautiful book. Good paper, gorgeous illustrations. The child is delighted. We will definitely buy more books from this publisher. Service and delivery at the level.", Reviewer="Mary"}
                });

            modelBuilder.Entity<Rating>().HasData(
                new Rating[]
                {
                    new Rating { Book = books[0], Score=10},
                    new Rating{ Book = books[1], Score=10},
                    new Rating { Book = books[2], Score=9}
                });
        }
    }
}