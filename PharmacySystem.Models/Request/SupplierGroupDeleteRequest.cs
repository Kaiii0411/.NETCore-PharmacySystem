using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.Models.Request
{
    public class SupplierGroupDeleteRequest
    {
        public long IdSupplierGroup { get; set; }
        public string? SupplierGroupName { get; set; }
    }
}
