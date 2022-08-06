using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }
        public string Message { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public string Reviewer { get; set; }
    }
}
