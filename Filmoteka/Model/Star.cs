using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Filmoteka.Model
{
    public class Star: Person
    {
        public Star(int id, string firstName, string lastName, string gender, string dateOfBirth, string birthPlace, string bio, string nickname, BitmapImage image) :
            base(id, firstName, lastName, gender, dateOfBirth, birthPlace, bio, nickname, image)
        {

        }

        public Star() : base() { }

        public override string ToString() => FirstName + " " + LastName;

        public override bool Equals(object obj) => ((Star)obj).ID == ID;

    }
}
