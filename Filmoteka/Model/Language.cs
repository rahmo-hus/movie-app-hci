using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmoteka.Model
{
    public class Language
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public override string ToString() => Name;

        public Language() { }

        public Language(int iD, string name)
        {
            ID = iD;
            Name = name;
        }

        public override bool Equals(object obj) => ((Language)obj).ID == ID;
    }
}
