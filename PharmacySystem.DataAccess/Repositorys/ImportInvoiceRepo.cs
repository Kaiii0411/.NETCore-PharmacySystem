using PharmacySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.DataAccess.Repositorys
{
    public interface IImportInvoiceRepo : IRepository<ImportInvoice>
    {

    }
    public class ImportInvoiceRepo : GenericRepository<ImportInvoice>, IImportInvoiceRepo
    {
        public ImportInvoiceRepo(PharmacySystemContext context) : base(context)
        {
        }
    }
}
