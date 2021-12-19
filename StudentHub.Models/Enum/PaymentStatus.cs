using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHub.Models.Enum
{
    public enum PaymentStatus
    {
        Initiated = 1,
        Pending = 2,
        Success = 3,
        Failed = 4,
        Cancelled = 5
    }
}
