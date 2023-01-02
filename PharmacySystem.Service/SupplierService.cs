using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PharmacySystem.DataAccess.Repositorys;
using PharmacySystem.Models;
using PharmacySystem.Models.Common;
using PharmacySystem.Models.Request;
using PharmacySystem.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.Service
{
    public interface ISupplierService
    {
        Task<long> Create(SupplierCreateRequest request);
        Task<long> Update(SupplierUpdateRequest request);
        Task<long> Delete(long supplierId);
        Task<IEnumerable<Supplier>> GetListSupplier();
        Task<PagedResult<SupplierVM>> Get(GetManageSupplierPagingRequest request);
        Task<Supplier> GetByID(long SupplierId);
        Task<List<SupplierVM>> GetListBuSupplierGroup(long SupplierGroupId);
    }
    public class SupplierService : ISupplierService
    {
        private readonly PharmacySystemContext _context;
        private readonly ISupplierRepo _supplierRepo;
        private readonly IMapper _mapper;

        public SupplierService(PharmacySystemContext context, ISupplierRepo supplierRepo, IMapper mapper)
        {
            this._context = context;
            this._supplierRepo = supplierRepo;
            this._mapper = mapper;
        }
        public async Task<long> Create(SupplierCreateRequest request)
        {
            var Supplier = new Supplier()
            {
                SupplierName = request.SupplierName,
                Address = request.Address,
                Phone = request.Phone,
                Email = request.Email,
                IdSupplierGroup = request.IdSupplierGroup
            };
            _context.Suppliers.Add(Supplier);
            await _context.SaveChangesAsync();
            return Supplier.IdSupplier;
        }
        public async Task<long> Update(SupplierUpdateRequest request)
        {
            var supplier = await _context.Suppliers.FindAsync(request.IdSupplier);
            if (supplier == null)
                return 0;
            supplier.SupplierName = request.SupplierName;
            supplier.Address = request.Address;
            supplier.Phone = request.Phone;
            supplier.Email = request.Email;
            supplier.IdSupplierGroup = request.IdSupplierGroup;
            _context.Suppliers.Update(supplier);
            await _context.SaveChangesAsync();
            return supplier.IdSupplier;
        }
        public async Task<long> Delete(long supplierId)
        {
            var supplier = await _context.Suppliers.FindAsync(supplierId);
            if (supplier == null) return 0;
            _context.Suppliers.Remove(supplier);
            return await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Supplier>> GetListSupplier()
        {
            IReadOnlyList<Supplier> listSupplier = await _supplierRepo.ListAsync();
            return _mapper.Map<IEnumerable<Supplier>>(listSupplier);
        }
        public async Task<PagedResult<SupplierVM>> Get(GetManageSupplierPagingRequest request)
        {
            //select 
            var query = from s in _context.Suppliers
                        join sg in _context.SupplierGroups on s.IdSupplierGroup equals sg.IdSupplierGroup
                        select new { s, sg };
            //search
            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.s.SupplierName.Contains(request.Keyword));
            if (request.IdSupplierGroup != null && request.IdSupplierGroup != 0)
            {
                query = query.Where(x => x.s.IdSupplierGroup == request.IdSupplierGroup);
            }
            //list
            int totalRow = await query.CountAsync();
            var data = await query.Select(x => new SupplierVM()
            {
                IdSupplier = x.s.IdSupplier,
                SupplierName = x.s.SupplierName,
                Address = x.s.Address,
                Phone = x.s.Phone,
                Email = x.s.Email,
                SupplierGroupName = x.sg.SupplierGroupName
            }).ToListAsync();
            //data
            var pagedResult = new PagedResult<SupplierVM>()
            {
                TotalRecords = totalRow,
                Items = data
            };
            return pagedResult;
        }
        public async Task<Supplier> GetByID(long SupplierId)
        {
            var supllier = await _context.Suppliers.FindAsync(SupplierId);
            var supllierDetails = new Supplier()
            {
                 IdSupplier = SupplierId,
                 SupplierName =  supllier.SupplierName,
                 Address = supllier.Address,
                 Phone = supllier.Phone,
                 Email = supllier.Email,
                 IdSupplierGroup = supllier.IdSupplierGroup
            };
            return supllierDetails;
        }
        public async Task<List<SupplierVM>> GetListBuSupplierGroup(long SupplierGroupId)
        {
            var list = new List<SupplierVM>();
            var query = from s in _context.Suppliers select s;
            var listSupplier = await query.Where(x => x.IdSupplierGroup == SupplierGroupId).Select(x => new SupplierVM()
            {
                IdSupplier = x.IdSupplier,
                SupplierName = x.SupplierName,
                Address = x.Address,
                Phone = x.Phone,
                Email = x.Email
            }).ToListAsync();
            return listSupplier;
        }
    }
}
