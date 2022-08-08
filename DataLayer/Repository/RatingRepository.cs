using DataLayer.Context;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repository
{
    public class RatingRepository : IRepository<Rating>
    {
        private readonly BookstoreContext db;
        public async Task<IEnumerable<Rating>> GetAllAsync() => await db.Ratings.ToListAsync();

        public RatingRepository()
        {
            db = new BookstoreContext();
        }

        public async Task<Rating> Add(Rating rating)
        {
            var result = await db.Ratings.AddAsync(rating);
            await db.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Rating> Delete(Rating rating)
        {
            var result = await FindByIdAsync(rating.Id);
            if (result != null)
            {
                db.Ratings.Remove(rating);
                await db.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<Rating> Update(Rating rating)
        {
            var result = await db.Ratings
            .FirstOrDefaultAsync(e => e.Id == rating.Id);

            if (result != null)
            {
                result.Score = rating.Score;
                result.BookId = rating.BookId;
                db.Ratings.Update(result);
                await db.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<Rating> FindByIdAsync(int id)
        {
            return await db.Ratings.FirstOrDefaultAsync(e => e.Id == id);
        }
        public void Save()
        {
            db.SaveChanges();
        }
    }
}