using DataLayer.Dtos;
using DataLayer.Models;
using DataLayer.Repository;
using HomeTask2.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace HomeTask2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        readonly IRepository<Book> BookRepository;
        readonly IRepository<Rating> RatingRepository;
        readonly IRepository<Review> ReviewRepository;
        public BooksController()
        {
            BookRepository = new BookRepository();
            RatingRepository = new RatingRepository();
            ReviewRepository = new ReviewRepository();
        }

        [HttpGet]
        public IEnumerable<GetBooksResponse> GetBooks(string order)
        {
            var data = BookRepository.All;
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

            List<GetBooksResponse> res = new List<GetBooksResponse>();
            if (order == "author")
            {
                res = books.OrderBy(o => o.Author).ToList();
            }
            else if (order == "title")
            {
                res = books.OrderBy(o => o.Title).ToList();
            }
            return res;
        }

        [HttpGet("{id:int}")]
        public BookDetailsResponse GetBookDetails(int id)
        {
            var data = BookRepository.FindById(id);
            var ratings = RatingRepository.All.Where(x => (x.BookId == data.Id)).ToList();
            var rate = ratings.Select(x =>
                        x.Score).Sum() / ratings.Count();
            var reviews = ReviewRepository.All.Where(x => (x.BookId == data.Id))
                .Select(x => new ReviewDto { Id = x.Id, Message = x.Message, Reviewer = x.Reviewer }).ToList();
            
            BookDetailsResponse book = new BookDetailsResponse
            {
                Id = data.Id,
                Title = data.Title,
                Author = data.Author,
                Cover = data.Cover,
                Content = data.Content,
                Rating = rate,
                Reviews = reviews
             };
            return book;
        }
    }
}
