using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.Models.Request
{
    public class MedicineGroupUpdateRequest
    {
        public long IdMedicineGroup { get; set; }
        public string MedicineGroupName { get; set; }
        public string? Note { get; set; }
    }
}
