﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationEFTest.Entity
{
    public partial class User:BaseEntity
    {

        public string Name { get; set; }

        public int Sex { get; set; }

        public double Age { get; set; }


       
    }
}
