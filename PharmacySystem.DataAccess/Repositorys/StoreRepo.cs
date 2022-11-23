using PharmacySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.DataAccess.Repositorys
{
    public interface IStoreRepo : IRepository<Store>
    {

    }
    public class StoreRepo: GenericRepository<Store>, IStoreRepo
    {
        public StoreRepo(PharmacySystemContext context) : base(context)
        {
        }
    }
}
