using PharmacySystem.Models;
using PharmacySystem.Models.Request;
using System;
using System.Collections.Generic;
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
    }
    public class StaffService : IStaffService
    {
        private readonly PharmacySystemContext _context;
        public StaffService(PharmacySystemContext context)
        {
            this._context = context;
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
    }
}
