using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.Models.Request
{
    public class RoleCreateRequest
    {
        public string RoleName { get; set; }
        public string? Description { get; set; }
    }
}
