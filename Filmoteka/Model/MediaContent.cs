using Filmoteka.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Filmoteka.Model
{
   public  class MediaContent
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public Language Language { get; set; }

        public Country OriginCountry { get; set; }

        public float Budget { get; set; }

        public int NumberOfWeeksRunning { get; set; }

        public BitmapImage Image { get; set; }

        public List<Genre> Genres { get; set; }

        public List<Star> Stars { get; set; }

        public List<Producer> Producers { get; set; }

        public List<Writer> Writers { get; set; }

        public List<Rating> Ratings { get; set; }

        public string StarsString { get
            {
                string list = string.Empty;
                Stars.ForEach(star => list += star + ",");
                return list[0..^1];
            } 
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
