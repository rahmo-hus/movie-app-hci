using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmoteka.Model
{
    public class Movie
    {
        public string Name { get; set; }
        
        public string Cast { get; set; }

        public string Image { get; set; }

        public Movie(string Name, string Cast, string Image)
        {
            this.Name = Name;
            this.Cast = Cast;
            this.Image = Image;
        }
    }
}
