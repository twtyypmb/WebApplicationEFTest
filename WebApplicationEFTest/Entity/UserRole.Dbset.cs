using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationEFTest.Entity
{
    public partial class UserRole
    {
        public virtual User UserCurrent
        {
            get; set;
        }
        public virtual User UserParent
        {
            get; set;
        }
        public virtual Role Role
        {
            get; set;
        }
    }
}
