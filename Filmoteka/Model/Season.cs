﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmoteka.Model
{
    public class Season
    {
        public int ID { get; set; }

        public List<Episode> Episodes { get; set; }
    }
}
