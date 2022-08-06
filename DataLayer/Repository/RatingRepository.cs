using DataLayer.Context;
using DataLayer.Models;

namespace DataLayer.Repository
{
    public class RatingRepository : IRepository<Rating>
    {
        private readonly BookstoreContext db;
        public IEnumerable<Rating> All => db.Ratings.ToList();

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

        public Rating FindById(int id)
        {
            return db.Ratings.FirstOrDefault(e => e.Id == id);
        }
        public void Save()
        {
            db.SaveChanges();
        }
    }
}