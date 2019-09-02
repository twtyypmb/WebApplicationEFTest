using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationEFTest.Entity
{
    public partial class OperateLog
    {
        public virtual User User1
        {
            get; set;
        }
        public virtual User User2
        {
            get; set;
        }
    }
}
