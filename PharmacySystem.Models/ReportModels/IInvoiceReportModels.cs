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
        public long IdImportInvoice { get; set; }
        public string DateCheckIn { get; set; }
        public string DateCheckOut { get; set; }
        public string SupplierName { get; set; }
        public string SupllierAddress { get; set; }
        public string SupplierPhone { get; set; }
        public string MedicineName { get; set; }
        public Nullable<double> ImportPrice { get; set; }
        public string Unit { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<double> TotalPrice { get; set; }
        public string StaffName { get; set; }
        public string StoreName { get; set; }
        public string StoreAddress { get; set; }
        public string StorePhone { get; set; }
        public string StoreOwner { get; set; }

    }
}
