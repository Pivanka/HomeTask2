using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models
{
    public class Rating
    {
        [Key]
        public int Id { get; set; }
        public Book Book { get; set; }
        public decimal Score { get; set; }
    }
}
