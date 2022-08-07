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

        public void Add(Rating rating)
        {
            db.Ratings.Add(rating);
        }

        public void Delete(Rating rating)
        {
            db.Ratings.Remove(rating);
        }

        public void Update(Rating rating)
        {
            db.Ratings.Update(rating);
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