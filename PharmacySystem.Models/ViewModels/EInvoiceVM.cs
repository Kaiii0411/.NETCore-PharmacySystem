using PharmacySystem.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.Models.ViewModels
{
    public class EInvoiceVM
    {
        public List<EInvoice> EInvoiceItems { get; set; }
        public ImportInvoiceCreateRequest CreateEInvoiceModel { get; set; }
    }
}
