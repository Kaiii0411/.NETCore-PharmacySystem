using PharmacySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.DataAccess.Repositorys
{
    public interface IMedicineGroupRepo: IRepository<MedicineGroup>
    {

    }
    public class MedicineGroupRepo: GenericRepository<MedicineGroup>, IMedicineGroupRepo
    {
        public MedicineGroupRepo(PharmacySystemContext context) : base(context)
        {
        }
    }
}
