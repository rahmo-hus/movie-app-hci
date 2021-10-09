using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmoteka.Model
{
    public class Genre
    {
        public int ID { get; set; }
        public string CategoryName { get; set; }

        public Genre (int id, string categoryName)
        {
            ID = id;
            CategoryName = categoryName;
        }

        public override string ToString() => CategoryName;
    }
}
