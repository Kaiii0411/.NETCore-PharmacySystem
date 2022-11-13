using System;
using System.Collections.Generic;

namespace PharmacySystem.Models
{
    public partial class Medicine
    {
        public long IdMedicine { get; set; }
        public string? MedicineName { get; set; }
        public string? Description { get; set; }
        public long? IdMedicineGroup { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public long? Quantity { get; set; }
        public string? Unit { get; set; }
        public double? SellPrice { get; set; }
        public double? ImportPrice { get; set; }
        public long? IdSupplier { get; set; }

        public virtual MedicineGroup? IdMedicineGroupNavigation { get; set; }
        public virtual Supplier? IdSupplierNavigation { get; set; }
    }
}
