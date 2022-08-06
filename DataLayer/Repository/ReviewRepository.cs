using DataLayer.Context;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository
{
    public class ReviewRepository : IRepository<Review>
    {
        private readonly BookstoreContext db;
        public IEnumerable<Review> All => db.Reviews.ToList();

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

        public Review FindById(int id)
        {
            return db.Reviews.FirstOrDefault(e => e.Id == id);
        }
        public void Save()
        {
            db.SaveChanges();
        }
    }
}
