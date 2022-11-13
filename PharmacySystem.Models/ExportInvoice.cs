using System;
using System.Collections.Generic;

namespace PharmacySystem.Models
{
    public partial class ExportInvoice
    {
        public ExportInvoice()
        {
            InvoiceDetails = new HashSet<InvoiceDetail>();
        }

        public long IdExportInvoice { get; set; }
        public long? IdAccount { get; set; }
        public DateTime? DateCheckIn { get; set; }
        public DateTime? DateCheckOut { get; set; }
        public double? TotalPrice { get; set; }
        public string? Status { get; set; }
        public string? Note { get; set; }

        public virtual Account? IdAccountNavigation { get; set; }
        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }
    }
}
