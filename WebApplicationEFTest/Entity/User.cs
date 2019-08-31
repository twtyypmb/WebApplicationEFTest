using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationEFTest.Entity
{
    public class User:BaseEntity
    {

        public string Name { get; set; }

        public int Sex { get; set; }

        public double Age { get; set; }

        public virtual IList<UserRole> UserRoles1 { get; set; }
        public virtual IList<UserRole> UserRoles2 { get; set; }

    }
}
