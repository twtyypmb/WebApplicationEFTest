using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationEFTest.Entity
{
    public class UserRole:BaseEntity
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }


        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }
}
