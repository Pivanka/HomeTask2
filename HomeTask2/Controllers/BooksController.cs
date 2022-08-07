using DataLayer.Dtos;
using DataLayer.Models;
using DataLayer.Repository;
using HomeTask2.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace HomeTask2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IRepository<Book> BookRepository;
        private readonly IRepository<Rating> RatingRepository;
        private readonly IRepository<Review> ReviewRepository;
        private readonly IOptions<WebApiOptions> _webApiOptions;
        public BooksController(
            IRepository<Book> bookRepository,
            IRepository<Rating> ratingRepository,
            IRepository<Review> reviewRepository,
            IOptions<WebApiOptions> webApiOptions)
        {
            BookRepository = bookRepository;
            RatingRepository = ratingRepository;
            ReviewRepository = reviewRepository;
            _webApiOptions = webApiOptions;
        }

        //[HttpGet]
        //public IActionResult Get()
        //{
        //    return Content($"My Account Ivanka is {_webApiOptions.Value.ApiKey}");
        //}

        [HttpGet]
        public async Task<IActionResult> GetBooks(string order)
        {
            try
            {
                var data = await BookRepository.GetAllAsync();
                var ratings = await RatingRepository.GetAllAsync();
                var reviews = await RatingRepository.GetAllAsync();

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

                if (order == "author")
                {
                    books = books.OrderBy(o => o.Author).ToList();
                }
                else if (order == "title")
                {
                    books = books.OrderBy(o => o.Title).ToList();
                }
                return Ok(books);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        //[HttpGet("{id:int}")]
        //public BookDetailsResponse GetBookDetails(int id)
        //{
        //    var data = BookRepository.FindById(id);
        //    var ratings = RatingRepository.All.Where(x => (x.BookId == data.Id)).ToList();
        //    var rate = ratings.Select(x =>
        //                x.Score).Sum() / ratings.Count();
        //    var reviews = ReviewRepository.All.Where(x => (x.BookId == data.Id))
        //        .Select(x => new ReviewDto { Id = x.Id, Message = x.Message, Reviewer = x.Reviewer }).ToList();

        //    BookDetailsResponse book = new BookDetailsResponse
        //    {
        //        Id = data.Id,
        //        Title = data.Title,
        //        Author = data.Author,
        //        Cover = data.Cover,
        //        Content = data.Content,
        //        Rating = rate,
        //        Reviews = reviews
        //    };
        //    return book;
        //}

        ////[HttpDelete]//("{id:int}/{secret}")
        //[HttpDelete] //Todo
        //public int DeleteBook( int id, string secret)
        //{
        //    if(secret == _webApiOptions.Value.ApiKey)
        //    {
        //        try
        //        {
        //            var bookToDelete = BookRepository.FindById(id);

        //            if (bookToDelete == null)
        //            {
        //                throw new Exception($"Book with Id = {id} not found");
        //            }
        //            BookRepository.Delete(bookToDelete); // BookRepository method delete need retuen id
        //        }
        //        catch (Exception)
        //        {
        //            Console.WriteLine(StatusCode(StatusCodes.Status500InternalServerError,
        //                "Error deleting data"));
        //        }
        //        return 1;
        //    }
        //    else
        //    {
        //        return 0;
        //    }
        //}
    }
}
