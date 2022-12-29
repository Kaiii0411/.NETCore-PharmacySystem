using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.Models.Request
{
    public class RoleUpdateRequest
    {
        public RoleUpdateRequest()
        {
            Users = new List<string>();
        }
        public Guid Id { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public List<string> Users { get; set; }
    }
}
