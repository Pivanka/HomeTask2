using DataLayer.Context;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repository
{
    public class BookRepository : IRepository<Book> //SaveChanges!
    {
        private readonly BookstoreContext db;

        public async Task<IEnumerable<Book>> GetAllAsync() => await db.Books.ToListAsync();

        public BookRepository()
        {
            db = new BookstoreContext();
        }

        public async Task<Book> Add(Book book)
        {
            var result = await db.Books.AddAsync(book);
            await db.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Book> Delete(Book book)
        {
            var result = await FindByIdAsync(book.Id);
            if (result != null)
            {
                db.Books.Remove(book);
                await db.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<Book> Update(Book book)
        {
            var result = await db.Books
            .FirstOrDefaultAsync(e => e.Id == book.Id);

            if (result != null)
            {
                result.Author = book.Author;
                result.Cover = book.Cover;
                result.Content = book.Content;
                result.Genre = book.Genre;
                result.Title = book.Title;
                db.Books.Update(result);
                await db.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<Book> FindByIdAsync(int id)
        {
            return await db.Books.FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
