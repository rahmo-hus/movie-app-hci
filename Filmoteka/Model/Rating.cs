using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmoteka.Model
{
    public class Rating
    {
        public int UserId { get; set; }
        public int RatingScore { get; set; }
        public DateTime Timestamp { get; set; }

    }
}
