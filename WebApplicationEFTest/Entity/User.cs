using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationEFTest.Entity
{
    public partial class User:BaseEntity
    {

        public string Name { get; set; }

        public int Sex { get; set; }

        public double Age { get; set; }


        public virtual IList<UserRole> UserRolesCurrent
        {
            get; set;
        }
        public virtual IList<UserRole> UserRolesParent
        {
            get; set;
        }
        public virtual IList<LoginLog> LoginLogs
        {
            get; set;
        }

        public virtual IList<OperateLog> ResourceOperateLogs1
        {
            get; set;
        }
        public virtual IList<OperateLog> ResourceOperateLogs2
        {
            get; set;
        }

        public virtual UserExtraInfo UserExtraInfo
        {
            get; set;
        }
    }
}
