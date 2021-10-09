using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmoteka.Model
{
   public  class MediaContent
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public string ParentalGuidance { get; set; }

        public int Popularity { get; set; }

        public string Description { get; set; }

        public Language Language { get; set; }

        public Country OriginCountry { get; set; }

        public string FilmingDates { get; set; }

        public float Budget { get; set; }

        public int NumberOfWeeksRunning { get; set; }

        public byte[] Image { get; set; }

        public List<Genre> Genres { get; set; }

        public List<Star> Stars { get; set; }

        public List<Producer> Producers { get; set; }

        public List<Writer> Writers { get; set; }

        public List<Review> Reviews { get; set; }

        public MediaContent(int iD, string name, string description, Language language, Country originCountry, float budget, List<Genre> genres, List<Star> stars, List<Producer> producers, List<Review> reviews)
        {
            ID = iD;
            Name = name;
            Description = description;
            Language = language;
            OriginCountry = originCountry;
            Budget = budget;
            Genres = genres;
            Stars = stars;
            Producers = producers;
            Reviews = reviews;
        }

        public MediaContent(string name, string description, Language language, Country originCountry, float budget, List<Genre> genres, List<Star> stars, List<Producer> producers)
        {
            Name = name;
            Description = description;
            Language = language;
            OriginCountry = originCountry;
            Budget = budget;
            Genres = genres;
            Stars = stars;
            Producers = producers;
        }

        public MediaContent()
        {

        }


        //check Rating ID



    }
}
