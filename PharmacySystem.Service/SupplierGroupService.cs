using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PharmacySystem.DataAccess.Repositorys;
using PharmacySystem.Models;
using PharmacySystem.Models.Common;
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
        Task<IEnumerable<SupplierGroup>> GetListSupplierGroup();
        Task<PagedResult<SupplierGroup>> Get(GetManageKeywordPagingRequest request);
    }
    public class SupplierGroupService : ISupplierGroupService
    {
        private readonly PharmacySystemContext _context;
        private readonly ISupplierGroupRepo _supplierGroupRepo;
        private readonly IMapper _mapper;
        public SupplierGroupService(PharmacySystemContext context, ISupplierGroupRepo supplierGroupRepo, IMapper mapper)
        {
            this._context = context;
            this._supplierGroupRepo = supplierGroupRepo;
            this._mapper = mapper;
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
        public async Task<IEnumerable<SupplierGroup>> GetListSupplierGroup()
        {
            IReadOnlyList<SupplierGroup> listMedicineGroup = await _supplierGroupRepo.ListAsync();
            return _mapper.Map<IEnumerable<SupplierGroup>>(listMedicineGroup);
        }
        public async Task<PagedResult<SupplierGroup>> Get(GetManageKeywordPagingRequest request)
        {
            //select
            var query = from sg in _context.SupplierGroups
                        select sg;
            //search
            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.SupplierGroupName.Contains(request.Keyword));
            //list
            int totalRow = await query.CountAsync();
            var data = await query.Select(x => new SupplierGroup()
            {
                IdSupplierGroup = x.IdSupplierGroup,
                SupplierGroupName = x.SupplierGroupName,
                Note = x.Note
            }).ToListAsync();
            //data
            var pagedResult = new PagedResult<SupplierGroup>()
            {
                TotalRecords = totalRow,
                Items = data
            };
            return pagedResult;
        }

    }
}
