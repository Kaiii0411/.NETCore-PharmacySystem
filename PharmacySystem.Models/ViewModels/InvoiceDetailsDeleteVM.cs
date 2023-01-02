using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.Models.ViewModels
{
    public class InvoiceDetailsDeleteVM
    {
        public long? IdImportInvoice { get; set; }
        public long? IdExportInvoice { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }
    }
}
