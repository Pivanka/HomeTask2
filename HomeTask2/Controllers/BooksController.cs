using AutoMapper;
using DataLayer.Context;
using DataLayer.Models;
using DataLayer.Repository;
using HomeTask2.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


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
            var reviews = RatingRepository.All.Count();
            List<GetBooksResponse> books = new List<GetBooksResponse>();
            foreach (var item in data)
            {
                var r = ratings.Where(x => (x.BookId == item.Id)).ToList();
                var rate = r.Select(x =>
                        x.Score).Sum() / ratings.Count();
                books.Add(new GetBooksResponse
                {
                    Id = item.Id,
                    Title = item.Title,
                    Author = item.Author,
                    Rating = rate,
                    ReviewsNumber = reviews
                }); ;
            }
            #region
            // Настройка конфигурации AutoMapper
            //var config = new MapperConfiguration(cfg => 
            //    cfg.CreateMap<GetBooksResponse, Book>()
            //    .ForMember("Ratings", opt => opt.MapFrom(src => src.)));
            //var mapper = new Mapper(config);
            #endregion

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

        //[HttpGet("{id}")]
        //public async Task<ActionResult<Book>> GetBook(int id)
        //{
        //    var book = await db.Books.FindAsync(id);

        //    if (book == null)
        //    {
        //        return NotFound();
        //    }

        //    return book;
        //}
    }
}
