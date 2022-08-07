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

        //[HttpGet]
        //public IEnumerable<GetBooksResponse> GetTopBooks(string? filter)
        //{
        //    var data = BookRepository.All;
        //    if (filter != null)
        //    {
        //        data = data.Where(x => (x.Genre == filter)).ToList();
        //    }
        //    var ratings = RatingRepository.All;
        //    var reviews = ReviewRepository.All;
        //    List<GetBooksResponse> books = new List<GetBooksResponse>();
        //    foreach (var item in data)
        //    {
        //        var r = ratings.Where(x => (x.BookId == item.Id)).ToList();
        //        var rate = r.Select(x =>
        //                x.Score).Sum() / r.Count();
        //        var countReviews = reviews.Where(x => x.BookId == item.Id).Count();
        //        books.Add(new GetBooksResponse
        //        {
        //            Id = item.Id,
        //            Title = item.Title,
        //            Author = item.Author,
        //            Rating = rate,
        //            ReviewsNumber = countReviews
        //        }); ;
        //    }

        //    books = books.Where(x => x.ReviewsNumber > 0).OrderByDescending(x => x.Rating).ToList(); //Must be 10!
        //    books = books.Take(10).ToList();
        //    return books;
        //}
    }
}
