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

        public string Language { get; set; }

        public string OriginCountry { get; set; }

        public string FilmingDates { get; set; }

        public float Budget { get; set; }

        public int NumberOfWeeksRunning { get; set; }

        public byte[] Image { get; set; }

        public List<Genre> Genres { get; set; }

        public List<Star> Stars { get; set; }

        public List<Producer> Producers { get; set; }

        public List<Writer> Writers { get; set; }

        public List<Review> Reviews { get; set; }

        public MediaContent(int iD, string name, string parentalGuidance, int popularity, string description, string language, string originCountry, string filmingDates, float budget, int numberOfWeeksRunning)
        {
            ID = iD;
            Name = name;
            ParentalGuidance = parentalGuidance;
            Popularity = popularity;
            Description = description;
            Language = language;
            OriginCountry = originCountry;
            FilmingDates = filmingDates;
            Budget = budget;
            NumberOfWeeksRunning = numberOfWeeksRunning;
        }

        public MediaContent(string name, string parentalGuidance, int popularity, string description, string language, string originCountry, string filmingDates, float budget, int numberOfWeeksRunning)
        {
            Name = name;
            ParentalGuidance = parentalGuidance;
            Popularity = popularity;
            Description = description;
            Language = language;
            OriginCountry = originCountry;
            FilmingDates = filmingDates;
            Budget = budget;
            NumberOfWeeksRunning = numberOfWeeksRunning;
        }

        public MediaContent()
        {

        }


        //check Rating ID



    }
}
