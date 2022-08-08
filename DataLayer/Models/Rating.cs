using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models
{
    public class Rating
    {
        [Key]
        public int Id { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        [Range(1, 5)]
        public decimal Score { get; set; }
    }
}
