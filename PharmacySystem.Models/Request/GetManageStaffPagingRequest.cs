using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.Models.Request
{
    public class GetManageStaffPagingRequest
    {
        public long? IdStaff { get; set; }
        public string? StaffName { get; set; }
    }
}
