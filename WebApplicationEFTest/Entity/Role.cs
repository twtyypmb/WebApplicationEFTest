using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationEFTest.Entity
{
    public partial class Role:BaseEntity
    {
        
        [Description("姓名")]
        public string Name { get; set; }

        [Description("卡类型")]
        public CardTypeEnum CardType { get; set; }
        
    }
}
