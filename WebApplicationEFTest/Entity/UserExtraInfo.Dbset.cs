using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationEFTest.Entity
{
    public partial class UserExtraInfo
    {
        [ForeignKey("UserId")]
        public virtual User User
        {
            get;set;
        }
    }
}
