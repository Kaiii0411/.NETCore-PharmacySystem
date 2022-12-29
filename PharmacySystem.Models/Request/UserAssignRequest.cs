using PharmacySystem.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.Models.Request
{
    public class UserAssignRequest
    {
        public Guid Id { get; set; }
        public List<SelectItem> Users { get; set; } = new List<SelectItem>();
    }
}
