using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.Models
{
    public class IInvoice
    {
        private readonly PharmacySystemContext _context;
        public IInvoice(PharmacySystemContext context)
        {
            this._context = context;
        }
        public long IIdMedicine { get; set; }
        public int IQuantity { get; set; }
        public double IPrice { get; set; }
        public double ITotalPrice { get { return IQuantity * IPrice; } }

        public IInvoice(long idMedicine)
        {
            IIdMedicine = idMedicine;
            Medicine medicine = _context.Medicines.Single( x => x.IdMedicine == idMedicine);
            IPrice = medicine.ImportPrice;
            IQuantity = 1;
        }
    }
}
