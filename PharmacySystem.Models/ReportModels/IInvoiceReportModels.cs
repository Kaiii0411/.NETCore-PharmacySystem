using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.Models.ReportModels
{
    public class IInvoiceReportModels
    {
        [Key]
        public long? IdImportInvoice { get; set; }
        public DateTime? DateCheckIn { get; set; }
        public DateTime? DateCheckOut { get; set; }
        public string MedicineName { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }
        public string SupplierName { get; set; }
    }
}
