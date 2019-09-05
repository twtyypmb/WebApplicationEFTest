using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationEFTest.Entity
{

    public enum CardTypeEnum
    {
        [Description("身份证")]
        ID_CARD,

        [Description("就诊卡")]
        MEDICAL_CARD
    }
}
