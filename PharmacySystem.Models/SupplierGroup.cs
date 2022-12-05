using System;
using System.Collections.Generic;

namespace PharmacySystem.Models
{
    public partial class SupplierGroup
    {
        public SupplierGroup()
        {
            Suppliers = new HashSet<Supplier>();
        }

        public long IdSupplierGroup { get; set; }
        public string SupplierGroupName { get; set; } = null!;
        public string? Note { get; set; }

        public virtual ICollection<Supplier> Suppliers { get; set; }
    }
}
