using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmoteka.Model
{
    public class Series:MediaContent
    {
        public List<Season> Seasons { get; set; }


        public Series() : base() { }


    }
}
