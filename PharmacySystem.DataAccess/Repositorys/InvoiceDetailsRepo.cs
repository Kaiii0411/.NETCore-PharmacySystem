using PharmacySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.DataAccess.Repositorys
{
    public interface IInvoiceDetailsRepo : IRepository<InvoiceDetail>
    {

    }
    public class InvoiceDetailsRepo: GenericRepository<InvoiceDetail>, IInvoiceDetailsRepo
    {
        public InvoiceDetailsRepo(PharmacySystemContext context) : base(context)
        {
        }
    }
}
