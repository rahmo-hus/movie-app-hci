using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmoteka.Model
{
    public class Producer: Person
    {

        public  List<ProductionHouse> ProductionHouses { get; set; }

        public List<MediaContent> MediaContents { get; set; }
        public Producer(int id, string firstName, string lastName, string gender, string dateOfBirth, string birthPlace, string bio, string nickname, List<ProductionHouse> productionHouses, List<MediaContent> mediaContents) :
                base(id, firstName, lastName, gender, dateOfBirth, birthPlace, bio, nickname)
        {
            ProductionHouses = productionHouses;
            MediaContents = mediaContents;
        }
    }
}
