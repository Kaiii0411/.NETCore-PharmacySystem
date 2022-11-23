using AutoMapper;
using PharmacySystem.DataAccess.Repositorys;
using PharmacySystem.Models;
using PharmacySystem.Models.Request;
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
    }
}
