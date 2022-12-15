using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.Models.ViewModels
{
    public class InvoiceDetailsVM
    {
        public long MedicineId { get; set; }
        public string MedicineName { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }
    }
}
