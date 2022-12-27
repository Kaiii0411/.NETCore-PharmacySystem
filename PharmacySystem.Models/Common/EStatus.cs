using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.Models.Common
{
    public enum EStatus
    {
        NEW = 1,
        ACTIVE = 2,
        INACTIVE = 3,
        APPROVED = 4,
        REJECTED = 5,
        DONE = 6,
        RECEIVED = 7,
        ERROR = 8
    }
}
