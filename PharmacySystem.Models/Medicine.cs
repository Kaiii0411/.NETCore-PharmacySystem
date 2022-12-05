using System;
using System.Collections.Generic;

namespace PharmacySystem.Models
{
    public partial class Medicine
    {
        public Medicine()
        {
            InvoiceDetails = new HashSet<InvoiceDetail>();
        }

        public long IdMedicine { get; set; }
        public string MedicineName { get; set; } = null!;
        public string? Description { get; set; }
        public long IdMedicineGroup { get; set; }
        public DateTime ExpiryDate { get; set; }
        public long Quantity { get; set; }
        public string Unit { get; set; } = null!;
        public double SellPrice { get; set; }
        public double ImportPrice { get; set; }
        public long IdSupplier { get; set; }

        public virtual MedicineGroup IdMedicineGroupNavigation { get; set; } = null!;
        public virtual Supplier IdSupplierNavigation { get; set; } = null!;
        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }
    }
}
