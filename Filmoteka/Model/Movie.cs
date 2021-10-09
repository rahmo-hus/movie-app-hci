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

        public Movie(int iD, string name, string description, Language language, Country originCountry, float budget, List<Genre> genres, List<Star> stars, List<Producer> producers, List<Review> reviews, int duration)
            : base(iD, name, description, language, originCountry, budget, genres, stars, producers, reviews)
        {
            Duration = duration;
        }

        public Movie(string name, string description, Language language, Country originCountry, float budget, List<Genre> genres, List<Star> stars, List<Producer> producers, int duration)
            :base(name, description, language, originCountry, budget, genres, stars,producers)
        {
            Duration = duration;
        }

    }
}
