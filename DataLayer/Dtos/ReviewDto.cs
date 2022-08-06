using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Dtos
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string Reviewer { get; set; }
    }
}
