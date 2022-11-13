using PharmacySystem.Models;
using PharmacySystem.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.Service
{
    public interface ISupplierGroupService
    {
        Task<long> Create(SupplierGroupCreateRequest request);
        Task<long> Update(SupplierGroupUpdateRequest request);
        Task<long> Delete(long supplierGroupId);
    }
    public class SupplierGroupService : ISupplierGroupService
    {
        private readonly PharmacySystemContext _context;
        public SupplierGroupService(PharmacySystemContext context)
        {
            this._context = context;
        }
        public async Task<long> Create(SupplierGroupCreateRequest request)
        {
            var SupplierGroup = new SupplierGroup()
            {
                SupplierGroupName = request.SupplierGroupName,
                Note = request.Note
            };
            _context.SupplierGroups.Add(SupplierGroup);
            await _context.SaveChangesAsync();
            return SupplierGroup.IdSupplierGroup;
        }
        public async Task<long> Update(SupplierGroupUpdateRequest request)
        {
            var supplierGroup = await _context.SupplierGroups.FindAsync(request.IdSupplierGroup);
            if (supplierGroup == null)
                return 0;
            supplierGroup.SupplierGroupName = request.SupplierGroupName;
            supplierGroup.Note = request.Note;
            _context.SupplierGroups.Update(supplierGroup);
            await _context.SaveChangesAsync();
            return supplierGroup.IdSupplierGroup;
        }
        public async Task<long> Delete(long supplierGroupId)
        {
            var supplierGroup = await _context.SupplierGroups.FindAsync(supplierGroupId);
            if (supplierGroup == null) return 0;
            _context.SupplierGroups.Remove(supplierGroup);
            return await _context.SaveChangesAsync();
        }
    }
}
