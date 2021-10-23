using Filmoteka.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmoteka.Model
{
    public class Movie: MediaContent
    {
        public int Duration { get; set; }

        public Movie() { }


        public Movie(string name, string description, Language language, Country originCountry, float budget, List<Genre> genres, List<Star> stars, List<Producer> producers, int duration)
            :base(name, description, language, originCountry, budget, genres, stars,producers)
        {
            Duration = duration;
        }

    }
}
