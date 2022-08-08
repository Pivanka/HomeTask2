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
        private readonly IRepository<Book> BookRepository;
        private readonly IRepository<Rating> RatingRepository;
        private readonly IRepository<Review> ReviewRepository;
        public RecommendedController(
            IRepository<Book> bookRepository,
            IRepository<Rating> ratingRepository,
            IRepository<Review> reviewRepository)
        {
            BookRepository = bookRepository;
            RatingRepository = ratingRepository;
            ReviewRepository = reviewRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetTopBooks(string? filter)
        {
            try
            {
                var data = await BookRepository.GetAllAsync();
                if (filter != null)
                {
                    data = data.Where(x => (x.Genre == filter)).ToList();
                }

                var ratings = await RatingRepository.GetAllAsync();
                var reviews = await ReviewRepository.GetAllAsync();

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

                books = books.Where(x => x.ReviewsNumber >= 10).OrderByDescending(x => x.Rating).ToList();
                books = books.Take(10).ToList();

                return Ok(books);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
