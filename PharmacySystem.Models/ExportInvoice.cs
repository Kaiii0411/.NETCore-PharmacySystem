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
        public long? IdStaff{ get; set; }
        public DateTime? DateCheckIn { get; set; }
        public DateTime? DateCheckOut { get; set; }
        public int StatusId { get; set; }
        public string? Note { get; set; }

        public virtual Status Status { get; set; } = null!;
        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }
    }
}
