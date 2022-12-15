using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.Models
{
    public class IInvoice
    {
        public long IInvoiceId { get; set; }
        public long IDetailsId { get; set; }
        public long IIdMedicine { get; set; }
        public string IMedicineName { get; set; }
        public int IQuantity { get; set; }
        public double IPrice { get; set; }
    }
}
