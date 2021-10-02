using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmoteka.Model
{
    public class Genre
    {
        public int ID { get; set; }
        public string CategoryName { get; set; }

        public List<Movie> Movies { get; set; }

        public Genre (int id, string categoryName, List<Movie> movies)
        {
            ID = id;
            CategoryName = categoryName;
            Movies = movies;
        }


    }
}
