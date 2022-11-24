using PharmacySystem.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.Models.Request
{
    public class GetManageMedicinePagingRequest
    {
        public string? Keyword { get; set; }
        public long? IdMedicineGroup { get; set; }
        public long? IdSupplier { get; set; }
    }
}
