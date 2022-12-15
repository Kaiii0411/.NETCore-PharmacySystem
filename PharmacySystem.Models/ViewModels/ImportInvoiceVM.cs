using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.Models.ViewModels
{
    public class ImportInvoiceVM
    {
        public long IdImportInvoice { get; set; }
        public string UserName { get; set; }
        public string DateCheckIn { get; set; }
        public string DateCheckOut { get; set; }
        public string StatusName { get; set; }
        public string StatusColor { get; set; }
        public string StatusText { get; set; }
        public string? Note { get; set; }
        public long? IdSupllier { get; set; }
        public string Supplier { get; set; }
        public List<InvoiceDetailsVM> invoiceDetails { get; set; }
    }
}
