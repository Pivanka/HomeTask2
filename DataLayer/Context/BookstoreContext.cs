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
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionalBiulder)
        {
            var connectionStringBuilder = new SqlConnectionStringBuilder
            {
                ["Server"] = "DESKTOP-P7QOF3N",
                ["DataBase"] = "Bookstore",
                ["Trusted_Connection"] = true,
                ["Encrypt"] = false
            };

            Console.WriteLine(connectionStringBuilder.ConnectionString);

            optionalBiulder
                .UseSqlServer(connectionStringBuilder.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
            .HasMany(p => p.Reviews)
            .WithOne(c => c.Book)
            .IsRequired();

            modelBuilder.Entity<Book>()
                .HasMany(s => s.Ratings)
                .WithOne(s => s.Book)
                .IsRequired();

            Book[] books = new Book[]
                {
                    new Book { Id = 1, Title = "1984", Author = "Goerge Orwell", Cover="https://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1327144697l/3744438.jpg", Content="1984 is a dystopian novella by George Orwell published in 1949, which follows the life of Winston Smith, a low ranking member of 'the Party', who is frustrated by the omnipresent eyes of the party, and its ominous ruler Big Brother. 'Big Brother' controls every aspect of people's lives.", Genre="novel"},
                    new Book { Id = 2, Title = "The Little Prince", Author = "Antoine de Saint-Exupéry", Cover="https://images-na.ssl-images-amazon.com/images/I/71OZY035QKL.jpg", Content="The Little Prince is an honest and beautiful story about loneliness, friendship, sadness, and love. The prince is a small boy from a tiny planet (an asteroid to be precise), who travels the universe, planet-to-planet, seeking wisdom. On his journey, he discovers the unpredictable nature of adults.", Genre="novella"},
                    new Book { Id = 3, Title = "Harry Potter", Author = "J. K. Rowling", Cover="https://images.ctfassets.net/usf1vwtuqyxm/3d9kpFpwHyjACq8H3EU6ra/85673f9e660407e5e4481b1825968043/English_Harry_Potter_4_Epub_9781781105672.jpg?w=914&q=70&fm=jpg", Content="The novels chronicle the lives of a young wizard, Harry Potter, and his friends Hermione Granger and Ron Weasley, all of whom are students at Hogwarts School of Witchcraft and Wizardry. The main story arc concerns Harry's struggle against Lord Voldemort, a dark wizard who intends to become immortal, overthrow the wizard governing body known as the Ministry of Magic and subjugate all wizards and Muggles (non-magical people).", Genre="fantasy novel"},
                    new Book { Id = 4, Title = "The Man Who Died Twice", Author = "Richard Osman", Cover = "https://storage.googleapis.com/lr-assets/_nielsen/400/9780241988244.jpg", Content = "A fabulously twisty crime novel that also sits in our feel-good and humorous categories, what more could you ask for?", Genre = "crime novel"},
                    new Book { Id = 5, Title ="Ugly Love", Author="Colleen Hoover", Cover="https://storage.googleapis.com/lr-assets/_nielsen/400/9781471136726.jpg", Content="When Tate Collins finds airline pilot Miles Archer passed out in front of her apartment door, it is definitely not love at first sight. They wouldn't even go so far as to consider themselves friends. But what they do have is an undeniable mutual attraction.He doesn't want love and she doesn't have time for a relationship, but their chemistry cannot be ignored.Once their desires are out in the open, they realize they have the perfect set - up, as long as Tate can stick to two rules:Never ask about the past and don't expect a future.Tate is determined that she can handle it, but when she realises that she can't, will she be able to say no to her sexy pilot when he lives just next door?", Genre="novel"},
                    new Book { Id = 6, Title ="The Hunger Games", Author="Suzanne Collins", Cover="https://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1586722975l/2767052.jpg", Content="In the ruins of a place once known as North America lies the nation of Panem, a shining Capitol surrounded by twelve outlying districts. The Capitol is harsh and cruel and keeps the districts in line by forcing them all to send one boy and one girl between the ages of twelve and eighteen to participate in the annual Hunger Games, a fight to the death on live TV.", Genre="science fiction"},
                    new Book { Id = 7, Title ="Divergent", Author="Veronica Roth", Cover="https://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1618526890l/13335037._SY475_.jpg", Content="In Beatrice Prior's dystopian Chicago world, society is divided into five factions, each dedicated to the cultivation of a particular virtue—Candor (the honest), Abnegation (the selfless), Dauntless (the brave), Amity (the peaceful), and Erudite (the intelligent). On an appointed day of every year, all sixteen-year-olds must select the faction to which they will devote the rest of their lives. For Beatrice, the decision is between staying with her family and being who she really is—she can't have both. So she makes a choice that surprises everyone, including herself.", Genre="Fantasy"},
                    new Book { Id = 8, Title ="Twilight", Author="Stephenie Meyer", Content="About three things I was absolutely positive.First, Edward was a vampire.Second, there was a part of him—and I didn't know how dominant that part might be—that thirsted for my blood.And third, I was unconditionally and irrevocably in love with him.", Cover="https://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1361039443l/41865.jpg", Genre="fantasy"},
                    new Book { Id = 9, Title ="The Great Gatsby", Author="F. Scott Fitzgerald", Content="The Great Gatsby, F. Scott Fitzgerald's third book, stands as the supreme achievement of his career. This exemplary novel of the Jazz Age has been acclaimed by generations of readers. The story of the fabulously wealthy Jay Gatsby and his love for the beautiful Daisy Buchanan, of lavish parties on Long Island at a time when The New York Times noted \"gin was the national drink and sex the national obsession,\" it is an exquisitely crafted tale of America in the 1920s.", Cover="https://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1490528560l/4671._SY475_.jpg", Genre="fiction"},
                    new Book { Id = 10, Title ="The Diary of a Young Girl", Author="Anne Frank", Content="In 1942, with the Nazis occupying Holland, a thirteen-year-old Jewish girl and her family fled their home in Amsterdam and went into hiding.", Cover="https://i.gr-assets.com/images/S/compressed.photo.goodreads.com/books/1560816565l/48855.jpg", Genre="biography"}
                };
            modelBuilder.Entity<Book>().HasData(books);

            Review[] reviews = new Review[]
                {
                    new Review { Id = 1, BookId = 1, Message = "This novel is another dystopia by George Orwell. I read the book after Orwell's \"Collective Animal Farm\". In this and that book, you can contemplate the terrible model of the Soviet Union or other totalitarian states. However, in the novel \"1984\" it is depicted much more deeply. Therefore, sometimes when you read, you cannot believe that humanity has really lost such a model of behavior and only a few are able to fight against it. However, will this lead to victory and the overthrow of the system? After reading this work, I can only say that it is always worth fighting against what does not reach you. Do not be silent. After all, there can be thousands or millions of people like you, and together you can achieve much more than you can alone. Definitely recommend reading!", Reviewer="Ksyusha"},
                    new Review { Id = 2, BookId = 3, Message = "Harry Potter is not just a story about a boy who lost his parents early. This series of books is about a great magician who went through a thorny path from an ordinary child who lived in a pantry under the stairs to a man with a capital letter, whose fate and exploits everyone knew, respected, admired.", Reviewer="John"},
                    new Review { Id = 3, BookId = 2, Message = "A beautiful book. Good paper, gorgeous illustrations. The child is delighted. We will definitely buy more books from this publisher. Service and delivery at the level.", Reviewer="Mary"},
                    new Review { Id = 4, BookId = 4, Message = "The second mystery solved by a group of senior citizens is even better than the first one. The four friends manage to find, with a little support from the police, the culprit behind a double murder. I did enjoy the interactions and the way the magnificent four perceive the world. Some moments had me chuckle for which I am grateful to Elizabeth, Joyce, Ron and Ibrahim.Highly recommend!", Reviewer = "Beata" },
                    new Review { Id = 5, BookId = 4, Message = "My favorite crime fighter septuagenarian foursome is back for another complicated case are ready with more than few tricks up their sleeves. Love it", Reviewer="Nill"},
                    new Review { Id = 6, BookId = 4, Message = "I LOVED THIS!I need to reread the first book cause I gave that 3.5 stars. Was I wrong? Or does the second book just have my specific tropes?The characters are smart and witty and chaotic. The plot had me sad then happy at breakneck turns. These characters are so compelling and wholesome while also being the type of morally grey that only truly intelligent people are.", Reviewer="Katie Colson"},
                    new Review { Id = 7, BookId = 5, Message = "This book was miserable. It goes toe to toe with Hopeless for the worst book I’ve read by her. There are three things that I think went horribly in this book.", Reviewer="Ayman"},
                    new Review { Id = 8, BookId = 5, Message = "Please excuse me while I try to pick up the pieces of my broken heart o m g this book", Reviewer="Hailey"},
                    new Review { Id = 9, BookId = 6, Message = "Very Good", Reviewer="Jayson"},
                    new Review { Id = 10, BookId = 6, Message = "she really was the blueprint", Reviewer="Ariel"},
                    new Review { Id = 11, BookId = 7, Message = "I definitely enjoyed it. At first, I had trouble convincing my older son to read it, because he was convinced that every dystopian novel is a \"Hunger Games\" wannabe, but he read it on a recent plane trip and we had a great in-depth discussion about the characters and their motivations.", Reviewer="Rick"},
                    new Review { Id = 12, BookId = 7, Message = "One of my first reads. Enjoyed every second reading this.", Reviewer="Nick"},
                    new Review { Id = 13, BookId = 8, Message = "wow. ten years later and im still absolute trash for edward cullen!?!? i guess my love for EC is just as immortal as he his. hehehe.", Reviewer="Jessica"},
                    new Review { Id = 14, BookId = 9, Message = "This is a good book, though it is so ridiculously overrated.", Reviewer="Sean Barrs"},
                    new Review { Id = 15, BookId = 9, Message = "There was one thing I really liked about The Great Gatsby. It was short.", Reviewer="Inge"},
                    new Review { Id = 16, BookId = 9, Message = "After six years of these heated and polarized debates, I'm deleting the reviews that sparked them. Thanks for sharing your frustrations, joys, and insights with me, goodreaders. Happy reading!", Reviewer="Lisa"},
                    new Review{ Id = 17,BookId = 10, Message = "When i was reading this book it didnt actually feel like a true story until i got to the end and i felt like i was missing the last 20 pages - then it sunk in what had happened. I sobbed while reading the Afterword. A must read.", Reviewer="Michelle"}
                };
            modelBuilder.Entity<Review>().HasData(reviews);

            modelBuilder.Entity<Rating>().HasData(
                new Rating[]
                {
                    new Rating { Id = 1, BookId = 1, Score=5},
                    new Rating { Id = 2, BookId = 1, Score=4},
                    new Rating { Id = 3, BookId = 2, Score=4},
                    new Rating { Id = 4, BookId = 2, Score=3},
                    new Rating { Id = 5, BookId = 3, Score=5},
                    new Rating { Id = 6, BookId = 3, Score=4},
                    new Rating { Id = 7, BookId = 3, Score=2},
                    new Rating { Id = 8, BookId = 4, Score=4},
                    new Rating { Id = 9, BookId = 4, Score=4},
                    new Rating { Id = 10, BookId = 5, Score=5},
                    new Rating { Id = 11, BookId = 5, Score=5},
                    new Rating { Id = 12, BookId = 6, Score=4},
                    new Rating { Id = 13, BookId = 6, Score=4},
                    new Rating { Id = 14, BookId = 7, Score=5},
                    new Rating { Id = 15, BookId = 7, Score=4},
                    new Rating { Id = 16, BookId = 7, Score=2},
                    new Rating { Id = 17, BookId = 8, Score=3},
                    new Rating { Id = 18, BookId = 9, Score=4},
                    new Rating { Id = 19, BookId = 9, Score=3},
                    new Rating { Id = 20, BookId = 10, Score=5},
                    new Rating { Id = 21, BookId = 10, Score=4},
                });
        }
    }
}