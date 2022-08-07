using DataLayer.Context;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repository
{
    public class BookRepository : IRepository<Book>
    {
        private readonly BookstoreContext db;

        public async Task<IEnumerable<Book>> GetAllAsync() => await db.Books.ToListAsync();

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

        public async Task<Book> FindByIdAsync(int id)
        {
            return await db.Books.FirstOrDefaultAsync(e => e.Id == id);
        }
        public void Save()
        {
            db.SaveChanges();
        }
    }
}
