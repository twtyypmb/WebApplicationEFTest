﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationEFTest.Entity
{
    public partial class LoginLog
    {
        public virtual User User
        {
            get; set;
        }
    }
}
