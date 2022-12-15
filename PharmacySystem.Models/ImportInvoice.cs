using System;
using System.Collections.Generic;

namespace PharmacySystem.Models
{
    public partial class ImportInvoice
    {
        public ImportInvoice()
        {
            InvoiceDetails = new HashSet<InvoiceDetail>();
        }

        public long IdImportInvoice { get; set; }
        public long IdAccount { get; set; }
        public DateTime DateCheckIn { get; set; }
        public DateTime DateCheckOut { get; set; }
        public int StatusId { get; set; }
        public string? Note { get; set; }
        public long IdSupplier { get; set; }

        public virtual Account IdAccountNavigation { get; set; } = null!;
        public virtual Supplier IdSupplierNavigation { get; set; } = null!;
        public virtual Status Status { get; set; } = null!;
        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }
    }
}
