using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationEFTest.Entity
{
    public class UserRole:BaseEntity
    {
        public string UserIdCurrent { get; set; }
        public string UserIdParent { get; set; }
        public string RoleId { get; set; }


        public virtual User UserCurrent { get; set; }
        public virtual User UserParent { get; set; }
        public virtual Role Role { get; set; }
    }
}
