using DataLayer.Context;
using DataLayer.Models;

namespace DataLayer.Repository
{
    public class BookRepository : IRepository<Book>
    {
        private readonly BookstoreContext db;
        public IEnumerable<Book> All => db.Books.ToList();

        public BookRepository()
        {
            db = new BookstoreContext();
        }

        public void Add(Book book)
        {
            db.Books.Add(book);
        }

        public void Delete(Book book)
        {
            db.Books.Remove(book);
        }

        public void Update(Book book)
        {
            db.Books.Update(book);
        }

        public Book FindById(int id)
        {
            return db.Books.FirstOrDefault(e => e.Id == id);
        }
        public void Save()
        {
            db.SaveChanges();
        }
    }
}
