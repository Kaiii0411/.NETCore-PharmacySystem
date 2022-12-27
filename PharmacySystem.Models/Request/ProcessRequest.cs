using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.Models.Request
{
    public class ProcessRequest
    {
        public long IdInvoice { get; set; }
        public string Status { get; set; }
    }
}
