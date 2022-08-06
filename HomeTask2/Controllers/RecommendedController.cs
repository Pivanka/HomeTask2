using DataLayer.Models;
using DataLayer.Repository;
using HomeTask2.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace HomeTask2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecommendedController : ControllerBase
    {
        readonly IRepository<Book> BookRepository;
        readonly IRepository<Rating> RatingRepository;
        readonly IRepository<Review> ReviewRepository;
        public RecommendedController()
        {
            BookRepository = new BookRepository();
            RatingRepository = new RatingRepository();
            ReviewRepository = new ReviewRepository();
        }

        [HttpGet]
        public IEnumerable<GetBooksResponse> GetTopBooks(string? filter)
        {
            var data = BookRepository.All;
            if (filter != null)
            {
                data = data.Where(x => (x.Genre == filter)).ToList();
            }
            var ratings = RatingRepository.All;
            var reviews = RatingRepository.All;
            List<GetBooksResponse> books = new List<GetBooksResponse>();
            foreach (var item in data)
            {
                var r = ratings.Where(x => (x.BookId == item.Id)).ToList();
                var rate = r.Select(x =>
                        x.Score).Sum() / r.Count();
                var countReviews = reviews.Where(x => x.BookId == item.Id).Count();
                books.Add(new GetBooksResponse
                {
                    Id = item.Id,
                    Title = item.Title,
                    Author = item.Author,
                    Rating = rate,
                    ReviewsNumber = countReviews
                }); ;
            }

            books = books.Where(x => x.ReviewsNumber > 0).OrderByDescending(x => x.Rating).ToList(); //Must be 10!
            //List<GetBooksResponse> res = new List<GetBooksResponse>();
            books = books.Take(10).ToList();
            return books;
        }
    }
}
