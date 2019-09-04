using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationEFTest.Entity
{
    public partial class UserRole
    {
        [ForeignKey("UserIdCurrent")]
        public virtual User UserCurrent
        {
            get; set;
        }

        [ForeignKey("UserIdParent")]
        public virtual User UserParent
        {
            get; set;
        }

        [ForeignKey("RoleId")]
        public virtual Role Role
        {
            get; set;
        }
    }
}
