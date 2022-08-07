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

        public void Add(Review review)
        {
            db.Reviews.Add(review);
        }

        public void Delete(Review review)
        {
            db.Reviews.Remove(review);
        }

        public void Update(Review review)
        {
            db.Reviews.Update(review);
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
