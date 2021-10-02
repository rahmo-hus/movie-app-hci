using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmoteka.Model
{
    public class Person
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string DateOfBirth { get; set; }
        public string BirthPlace { get; set; }
        public string Bio { get; set; }
        public string Nickname { get; set; }

        public Person(int iD, string firstName, string lastName, string gender, string dateOfBirth, string birthPlace, string bio, string nickname)
        {
            ID = iD;
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            DateOfBirth = dateOfBirth;
            BirthPlace = birthPlace;
            Bio = bio;
            Nickname = nickname;
        }

        public Person(string firstName, string lastName, string gender, string dateOfBirth, string birthPlace, string bio, string nickname)
        {
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            DateOfBirth = dateOfBirth;
            BirthPlace = birthPlace;
            Bio = bio;
            Nickname = nickname;
        }

        public Person()
        {

        }
    }
}
