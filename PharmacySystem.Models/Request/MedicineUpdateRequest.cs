using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.Models.Request
{
    public class MedicineUpdateRequest
    {
        public long IdMedicine { get; set; }
        public string MedicineName { get; set; }
        public string Description { get; set; }
        public long IdMedicineGroup { get; set; }
        public DateTime ExpiryDate { get; set; }
        public long Quantity { get; set; }
        public string Unit { get; set; }
        public double SellPrice { get; set; }
        public double ImportPrice { get; set; }
        public long IdSupplier { get; set; }
    }
}
