using PharmacySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.DataAccess.Repositorys
{
    public interface ISupplierGroupRepo : IRepository<SupplierGroup>
    {

    }
    public class SupplierGroupRepo : GenericRepository<SupplierGroup>, ISupplierGroupRepo
    {
        public SupplierGroupRepo(PharmacySystemContext context) : base(context)
        {
        }
    }
}
