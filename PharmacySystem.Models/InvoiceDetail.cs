using System;
using System.Collections.Generic;

namespace PharmacySystem.Models
{
    public partial class InvoiceDetail
    {
        public long IdInvoiceDetails { get; set; }
        public long? IdImportInvoice { get; set; }
        public long? IdExportInvoice { get; set; }
        public long IdMedicine { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }

        public virtual ExportInvoice? IdExportInvoiceNavigation { get; set; }
        public virtual ImportInvoice? IdImportInvoiceNavigation { get; set; }
        public virtual Medicine IdMedicineNavigation { get; set; } = null!;
    }
}
