using PharmacySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.DataAccess.Repositorys
{
    public interface IExportInvoiceRepo : IRepository<ExportInvoice>
    {

    }
    public class ExportInvoiceRepo : GenericRepository<ExportInvoice>, IExportInvoiceRepo
    {
        public ExportInvoiceRepo(PharmacySystemContext context) : base(context)
        {
        }
    }
}
