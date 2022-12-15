using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.Models.Details
{
    public class ImportInvoiceDetails
    {
        public ImportInvoice ImportInvoice { get; set; }
        public List<InvoiceDetail>? invoiceDetails { get; set; }
    }
}
