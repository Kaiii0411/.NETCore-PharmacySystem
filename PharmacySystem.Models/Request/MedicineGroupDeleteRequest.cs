using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.Models.Request
{
    public class MedicineGroupDeleteRequest
    {
        public long IdMedicineGroup { get; set; }
        public string? MedicineGroupName { get; set; }
    }
}
