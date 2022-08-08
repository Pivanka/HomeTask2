using DataLayer.Dtos;
using DataLayer.Models;
using DataLayer.Repository;
using FluentValidation;
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

        [HttpGet]
        public async Task<IActionResult> GetBooks(string? order)
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
                return BadRequest();
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetBookDetails(int id)
        {
            try
            {
                var data = await BookRepository.FindByIdAsync(id);
                var ratings = await RatingRepository.GetAllAsync();
                var reviews = await ReviewRepository.GetAllAsync();

                if (data == null)
                {
                    return NotFound();
                }
                else
                {
                    ratings = ratings.Where(x => (x.BookId == data.Id)).ToList();
                    var rate = ratings.Select(x =>
                                x.Score).Sum() / ratings.Count();

                    reviews = reviews.Where(x => (x.BookId == data.Id));
                    var reviewsRes = reviews.Select(x => new ReviewDto { Id = x.Id, Message = x.Message, Reviewer = x.Reviewer }).ToList();

                    BookDetailsResponse book = new BookDetailsResponse
                    {
                        Id = data.Id,
                        Title = data.Title,
                        Author = data.Author,
                        Cover = data.Cover,
                        Content = data.Content,
                        Rating = rate,
                        Reviews = reviewsRes
                    };
                    return Ok(book);
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        //POST https://{{baseUrl}}/api/books/save
        [HttpPost("save")]
        public async Task<IActionResult> SaveBook([FromBody] Book book)
        {
            try
            {
                var data = await BookRepository.GetAllAsync();
                if (book != null && ModelState.IsValid)
                {
                    var res = data.Where(x => x.Id == book.Id).ToList();
                    if (res.Count == 0)
                    {
                        await BookRepository.Add(book);
                    }
                    else
                    {
                        await BookRepository.Update(book);
                    }
                    return Ok(book.Id);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        //[HttpDelete]//("{id:int}/{secret}")
        [HttpDelete("{id:int}")] //Todo!
        public async Task<ActionResult<Book>> DeleteBook([FromRoute] int id, [FromQuery] string secret)
        {
            try
            {
                if (secret == _webApiOptions.Value.ApiKey)
                {
                    var bookToDelete = await BookRepository.FindByIdAsync(id);
                    if (bookToDelete == null)
                    {
                        return NotFound();
                    }
                    var deleted = await BookRepository.Delete(bookToDelete);
                    return Ok(deleted.Id);
                }
                else
                    return NotFound("Secret key is not correct");
            }
            catch (Exception ex)
            {
                return BadRequest();
                //StatusCode(500, "Internal server error");
            }
        }

        //PUT https:{{baseUrl}}/api/books/{id}/review
        [HttpPut("{id}/review")]
        public async Task<IActionResult> Review(int id, [FromBody] ReviewDto review)
        {
            try
            {
                if (review != null && ModelState.IsValid)
                {
                    Review result = new Review
                    {
                        Message = review.Message,
                        Reviewer = review.Reviewer,
                        BookId = id
                    };
                    await ReviewRepository.Add(result);
                    return Ok(result.Id);
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        //PUT https:{{baseUrl}}/api/books/{id}/rate
        [HttpPut("{id}/rate")]
        public async Task<IActionResult> Rate(int id, [FromBody] RatingDto rate)
        {
            try
            {
                if (rate != null && ModelState.IsValid)
                {
                    Rating result = new Rating
                    {
                        Score = rate.Score,
                        BookId = id
                    };
                    await RatingRepository.Add(result);
                    return Ok(result.Id);
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
