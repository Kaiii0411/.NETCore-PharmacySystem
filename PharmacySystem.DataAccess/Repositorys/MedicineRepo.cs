using PharmacySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.DataAccess.Repositorys
{
    public interface IMedicineRepo : IRepository<Medicine>
    {

    }
    public class MedicineRepo: GenericRepository<Medicine>, IMedicineRepo
    {
        public MedicineRepo(PharmacySystemContext context) : base(context)
        {
        }
    }
}
