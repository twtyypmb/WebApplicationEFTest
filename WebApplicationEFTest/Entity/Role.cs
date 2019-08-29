using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationEFTest.Entity
{
    public class Role:BaseEntity
    {
        public string Name { get; set; }
        public virtual IList<UserRole> UserRoles { get; set; }
    }
}
