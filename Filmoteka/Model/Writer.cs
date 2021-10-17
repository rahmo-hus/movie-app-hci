﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Filmoteka.Model
{
    public class Writer:Person
    {
        public List<MediaContent> MediaContents { get; set; }
        public Writer(int id, string firstName, string lastName, string gender, string dateOfBirth, string birthPlace, string bio, string nickname, List<MediaContent> mediaContents, BitmapImage image) :
    base(id, firstName, lastName, gender, dateOfBirth, birthPlace, bio, nickname, image)
        {
            MediaContents = mediaContents;
        }
    }
}
