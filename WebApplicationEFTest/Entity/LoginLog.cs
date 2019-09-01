using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationEFTest.Entity
{
    public class LoginLog:BaseEntity
    {
        public string UserId { get; set; }
        public DateTime LoginTime { get; set; }

        public DateTime? LogoutTime { get; set; }

        public virtual User User { get; set; }
    }
}
