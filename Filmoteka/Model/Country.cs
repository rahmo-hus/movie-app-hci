using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmoteka.Model
{
    public class Country
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public override string ToString() => Name;

        public Country()
        {
        }

        public Country(int iD, string name)
        {
            ID = iD;
            Name = name;
        }

        public override bool Equals(object obj) => ((Country)obj).ID == ID;
    }
}
