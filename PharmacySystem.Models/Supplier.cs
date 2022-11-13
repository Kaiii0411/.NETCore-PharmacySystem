using System;
using System.Collections.Generic;

namespace PharmacySystem.Models
{
    public partial class Supplier
    {
        public Supplier()
        {
            ImportInvoices = new HashSet<ImportInvoice>();
            Medicines = new HashSet<Medicine>();
        }

        public long IdSupplier { get; set; }
        public string? SupplierName { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public long? IdSupplierGroup { get; set; }

        public virtual SupplierGroup? IdSupplierGroupNavigation { get; set; }
        public virtual ICollection<ImportInvoice> ImportInvoices { get; set; }
        public virtual ICollection<Medicine> Medicines { get; set; }
    }
}
