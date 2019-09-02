using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationEFTest.Entity
{
    public partial class UserRole:BaseEntity
    {
        public string UserIdCurrent { get; set; }
        public string UserIdParent { get; set; }
        public string RoleId { get; set; }


        
    }
}
