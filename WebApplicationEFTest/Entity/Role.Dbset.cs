using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationEFTest.Entity
{
    public partial class Role
    {
        public virtual IList<UserRole> UserRoles
        {
            get; set;
        }
    }
}
