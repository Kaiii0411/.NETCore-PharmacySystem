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
    public interface IStaffService
    {
        Task<long> Create(StaffCreateRequest request);
        Task<long> Update(StaffUpdateRequest request);
        Task<long> Delete(long staffId);
        Task<PagedResult<StaffVM>> Get(GetManageStaffPagingRequest request);
        Task<staff> GetByID(long StaffId);
        Task<IEnumerable<staff>> GetListStaff();
    }
    public class StaffService : IStaffService
    {
        private readonly PharmacySystemContext _context;
        private readonly IStaffRepo _staffRepo;
        private readonly IMapper _mapper;
        public StaffService(PharmacySystemContext context, IStaffRepo staffRepo, IMapper mapper)
        {
            this._context = context;
            this._staffRepo = staffRepo;
            this._mapper = mapper;
        }
        public async Task<long> Create(StaffCreateRequest request)
        {
            var staff = new staff()
            {
                StaffName = request.StaffName,
                DateOfBirth = request.DateOfBirth,
                Phone = request.Phone,
                Email = request.Email,
                Status = request.Status,
                IdStore = request.IdStore
            };
            _context.staff.Add(staff);
            await _context.SaveChangesAsync();
            return staff.IdStaff;
        }
        public async Task<long> Update(StaffUpdateRequest request)
        {
            var staff = await _context.staff.FindAsync(request.IdStore);
            if (staff == null)
                return 0;
            staff.StaffName = request.StaffName;
            staff.DateOfBirth = request.DateOfBirth;
            staff.Phone = request.Phone;
            staff.Email = request.Email;
            staff.Status = request.Status;
            staff.IdStore = request.IdStore;
            _context.staff.Update(staff);
            await _context.SaveChangesAsync();
            return staff.IdStaff;
        }
        public async Task<long> Delete(long staffId)
        {
            var staff = await _context.staff.FindAsync(staffId);
            if (staff == null) return 0;
            _context.staff.Remove(staff);
            return await _context.SaveChangesAsync();
        }
        public async Task<PagedResult<StaffVM>> Get(GetManageStaffPagingRequest request)
        {
            //select
            var query = from s in _context.staff
                        join st in _context.Stores on s.IdStore equals st.IdStore
                        select new { s, st};

            //search
            if (!string.IsNullOrEmpty(request.StaffName))
                query = query.Where(x => x.s.StaffName.Contains(request.StaffName));
            if (request.IdStaff != null && request.IdStaff != 0)
            {
                query = query.Where(x => x.s.IdStaff== request.IdStaff);
            }

            //list
            int totalRow = await query.CountAsync();
            var data = await query.Select(x => new StaffVM()
            {
                StaffName = x.s.StaffName,
                DateOfBirth = x.s.DateOfBirth,
                Phone = x.s.Phone,
                Email = x.s.Email,
                Status = x.s.Status,
                StoreName = x.st.StoreName
            }).ToListAsync();
            //data
            var pagedResult = new PagedResult<StaffVM>()
            {
                TotalRecords = totalRow,
                Items = data
            };
            return pagedResult;
        }
        public async Task<staff> GetByID(long StaffId)
        {
            var staff = await _context.staff.FindAsync(StaffId);

            var staffDetails = new staff()
            {
                IdStaff = StaffId,
                StaffName = staff.StaffName,
                DateOfBirth = staff.DateOfBirth,
                Phone = staff.Phone,
                Email = staff.Email,
                Status = staff.Status,
                IdStore = staff.IdStore,
            };
            return staffDetails;
        }
        public async Task<IEnumerable<staff>> GetListStaff()
        {
            IReadOnlyList<staff> listStaff = await _staffRepo.ListAsync();
            return _mapper.Map<IEnumerable<staff>>(listStaff);
        }
    }
}
