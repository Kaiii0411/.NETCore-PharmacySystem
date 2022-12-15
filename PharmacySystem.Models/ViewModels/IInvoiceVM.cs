using PharmacySystem.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.Models.ViewModels
{
    public class IInvoiceVM
    {
        public List<IInvoice> IInvoiceItems { get; set; }
        public ImportInvoiceCreateRequest CreateIInvoiceModel { get; set; }
    }
}
