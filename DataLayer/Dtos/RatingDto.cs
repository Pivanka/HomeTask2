
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Dtos
{
    public class RatingDto
    {
        public int Id { get; set; }
        [Range(1, 5)]
        public decimal Score { get; set; }
    }
}
