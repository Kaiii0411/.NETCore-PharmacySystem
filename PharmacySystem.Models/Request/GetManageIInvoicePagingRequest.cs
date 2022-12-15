using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.Models.Request
{
    public class GetManageIInvoicePagingRequest
    {
        public DateTime? DateCheckIn { get; set; }
        public DateTime? DateCheckOut { get; set; }
        public long? IdSupplier { get; set; }
        public int? StatusID { get; set; }
    }
}
