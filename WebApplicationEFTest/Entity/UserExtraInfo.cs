﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationEFTest.Entity
{
    public partial class UserExtraInfo : BaseEntity
    {
        public string UserId
        {
            get; set;
        }

        public string Address { get; set; }
    }
}
