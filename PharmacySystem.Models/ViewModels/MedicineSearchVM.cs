using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.Models.ViewModels
{
    public class MedicineSearchVM
    {
        public List<MedicineVM> Medicines { get; set; }
        public string keyWord { get; set; }
        public long IdSupplier { get; set; }
        public long IdMedicineGroup { get; set; }
    }
}
