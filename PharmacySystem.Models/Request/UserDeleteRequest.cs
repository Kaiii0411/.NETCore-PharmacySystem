using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.Models.Request
{
    public class UserDeleteRequest
    {
        public Guid Id { get; set; }
        public long IdStaff { get; set; }
        public long IdAccount { get; set; }
    }
}
