using System;
using System.Collections.Generic;

namespace PharmacySystem.Models
{
    public partial class MedicineGroup
    {
        public MedicineGroup()
        {
            Medicines = new HashSet<Medicine>();
        }

        public long IdMedicineGroup { get; set; }
        public string? MedicineGroupName { get; set; }
        public string? Note { get; set; }

        public virtual ICollection<Medicine> Medicines { get; set; }
    }
}
