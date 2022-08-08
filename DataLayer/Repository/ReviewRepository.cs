using DataLayer.Context;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repository
{
    public class ReviewRepository : IRepository<Review>
    {
        private readonly BookstoreContext db;
        public async Task<IEnumerable<Review>> GetAllAsync() => await db.Reviews.ToListAsync();

        public ReviewRepository()
        {
            db = new BookstoreContext();
        }

        public async Task<Review> Add(Review review)
        {
            var result = await db.Reviews.AddAsync(review);
            await db.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Review> Delete(Review review)
        {
            var result = await FindByIdAsync(review.Id);
            if (result != null)
            {
                db.Reviews.Remove(review);
                await db.SaveChangesAsync();
                return result;
            }
            return null;
        }
        public async Task<Review> Update(Review review)
        {
            var result = await db.Reviews
            .FirstOrDefaultAsync(e => e.Id == review.Id);

            if (result != null)
            {
                result.Reviewer = review.Reviewer;
                result.Message = review.Message;
                result.BookId = review.BookId;
                db.Reviews.Update(result);
                await db.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<Review> FindByIdAsync(int id)
        {
            return await db.Reviews.FirstOrDefaultAsync(e => e.Id == id);
        }
        public void Save()
        {
            db.SaveChanges();
        }
    }
}
