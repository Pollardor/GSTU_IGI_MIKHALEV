﻿using IGILab1Norm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab2.Models
{
    public class IndexViewModel
    {
        public IEnumerable<Rent> Rents { get; set; }
        public Rent Rent { get; set; }
    }
}