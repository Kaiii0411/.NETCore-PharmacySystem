using PharmacySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.DataAccess.Repositorys
{
    public interface IStaffRepo : IRepository<staff>
    {

    }
    public class StaffRepo : GenericRepository<staff>, IStaffRepo
    {
        public StaffRepo(PharmacySystemContext context) : base(context)
        {
        }
    }
}
