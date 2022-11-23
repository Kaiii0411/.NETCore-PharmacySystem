using PharmacySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.DataAccess.Repositorys
{
    public interface ISupplierRepo : IRepository<Supplier>
    {

    }
    public class SupplierRepo: GenericRepository<Supplier>, ISupplierRepo
    {
        public SupplierRepo(PharmacySystemContext context) : base(context)
        {
        }
    }
}
