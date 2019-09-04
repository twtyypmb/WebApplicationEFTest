using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationEFTest.Entity
{
    public partial class OperateLog
    {
        [ForeignKey("UserId1")]
        public virtual User User1
        {
            get; set;
        }
        [ForeignKey("UserId2")]
        public virtual User User2
        {
            get; set;
        }
    }
}
