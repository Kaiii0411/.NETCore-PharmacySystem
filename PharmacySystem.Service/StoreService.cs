using PharmacySystem.Models;
using PharmacySystem.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.Service
{
    public interface IStoreService
    {
        Task<long> Create(StoreCreateRequest request);
        Task<long> Update(StoreUpdateRequest request);
        Task<long> Delete(long storeId);
    }
    public class StoreService : IStoreService
    {
        private readonly PharmacySystemContext _context;
        public StoreService(PharmacySystemContext context)
        {
            this._context = context;
        }
        public async Task<long> Create(StoreCreateRequest request)
        {
            var Store = new Store()
            {
                StoreName = request.StoreName,
                Address = request.Address,
                StoreOwner = request.StoreOwner,
                Phone = request.Phone
            };
            _context.Stores.Add(Store);
            await _context.SaveChangesAsync();
            return Store.IdStore;
        }
        public async Task<long> Update(StoreUpdateRequest request)
        {
            var store = await _context.Stores.FindAsync(request.IdStore);
            if (store == null)
                return 0;
            store.StoreName = request.StoreName;
            store.Address = request.Address;
            store.StoreOwner = request.StoreOwner;
            store.Phone = request.Phone;
            _context.Stores.Update(store);
            await _context.SaveChangesAsync();
            return store.IdStore;
        }
        public async Task<long> Delete(long storeId)
        {
            var store = await _context.Medicines.FindAsync(storeId);
            if (store == null) return 0;
            _context.Medicines.Remove(store);
            return await _context.SaveChangesAsync();
        }
    }
}
