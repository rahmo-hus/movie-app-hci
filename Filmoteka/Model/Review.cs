using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmoteka.Model
{
    public class Review
    {
        public int ID { get; set; }

        public string Subject { get; set; }

        public string AuthorFirstName { get; set; }

        public string AuthorLastName { get; set; }

        public string Country { get; set; }

        public DateTime Date { get; set; }

        public string Type { get; set; }


    }
}
