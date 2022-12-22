using AutoMapper;
using PharmacySystem.DataAccess.Repositorys;
using PharmacySystem.Models;
using PharmacySystem.Models.Common;
using PharmacySystem.Models.Request;
using PharmacySystem.Models.ViewModels;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
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
        Task<IEnumerable<Store>> GetListStore();
        Task<PagedResult<Store>> Get(GetManageStorePagingRequest request);
        Task<Store> GetByID(long StoreId);
    }
    public class StoreService : IStoreService
    {
        private readonly PharmacySystemContext _context;
        private readonly IStoreRepo _storeRepo;
        private readonly IMapper _mapper;

        public StoreService(PharmacySystemContext context, IStoreRepo storeRepo, IMapper mapper)
        {
            this._context = context;
            this._storeRepo = storeRepo;
            this._mapper = mapper;
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
            var store = await _context.Stores.FindAsync(storeId);
            if (store == null) return 0;
            _context.Stores.Remove(store);
            return await _context.SaveChangesAsync();
        }
        public async Task<PagedResult<Store>> Get(GetManageStorePagingRequest request)
        {
            //select
            var query = from s in _context.Stores 
                        select s;

            //search
            if (!string.IsNullOrEmpty(request.StoreName))
                query = query.Where(x => x.StoreName.Contains(request.StoreName));
            if (request.IdStore != null && request.IdStore != 0)
            {
                query = query.Where(x => x.IdStore == request.IdStore);
            }

            //list
            int totalRow = await query.CountAsync();
            var data = await query.Select(x => new Store()
            {
                IdStore = x.IdStore,
                StoreName = x.StoreName,
                Address = x.Address,
                StoreOwner = x.StoreOwner,
                Phone = x.Phone
            }).ToListAsync();
            //data
            var pagedResult = new PagedResult<Store>()
            {
                TotalRecords = totalRow,
                Items = data
            };
            return pagedResult;
        }
        public async Task<Store> GetByID(long StoreId)
        {
            var store = await _context.Stores.FindAsync(StoreId);

            var storeDetails = new Store()
            {
                IdStore = store.IdStore,
                StoreName = store.StoreName,
                Address = store.Address,
                StoreOwner = store.StoreOwner,
                Phone = store.Phone
            };
            return storeDetails;
        }
        public async Task<IEnumerable<Store>> GetListStore()
        {
            IReadOnlyList<Store> listStore = await _storeRepo.ListAsync();
            return _mapper.Map<IEnumerable<Store>>(listStore);
        }
    }
}
