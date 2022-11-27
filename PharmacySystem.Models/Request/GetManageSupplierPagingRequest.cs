using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.Models.Request
{
    public class GetManageSupplierPagingRequest
    {
        public string? Keyword { get; set; }
        public long? IdSupplierGroup { get; set; }
    }
}
