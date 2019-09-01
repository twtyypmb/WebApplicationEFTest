using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationEFTest.Entity
{
    public class OperateLog : BaseEntity
    {

        public string UserId1 { get; set; }
        public string UserId2 { get; set; }


        public virtual User User1 { get; set; }
        public virtual User User2 { get; set; }


    }
}
