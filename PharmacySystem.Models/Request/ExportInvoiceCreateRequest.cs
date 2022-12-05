using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.Models.Request
{
    public class ExportInvoiceCreateRequest
    {
        public long IdImportInvoice { get; set; }
        public long IdAccount { get; set; }
        public DateTime? DateCheckIn { get; set; }
        public DateTime? DateCheckOut { get; set; }
        public int StatusID { get; set; }
        public string? Note { get; set; }
        public List<InvoiceDetail> InvoiceDetails { set; get; } = new List<InvoiceDetail>();
    }
}
