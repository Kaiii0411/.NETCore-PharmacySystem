using PharmacySystem.Models;
using PharmacySystem.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.Service
{
    public interface IMedicineGroupService
    {
        Task<long> Create(MedicineGroupCreateRequest request);
        Task<long> Update(MedicineGroupUpdateRequest request);
        Task<long> Delete(long medicineGroupId);
    }
    public class MedicineGroupService : IMedicineGroupService
    {
        private readonly PharmacySystemContext _context;
        public MedicineGroupService(PharmacySystemContext context)
        {
            this._context = context;
        }
        public async Task<long> Create(MedicineGroupCreateRequest request)
        {
            var MedicineGroup = new MedicineGroup()
            {
                MedicineGroupName = request.MedicineGroupName,
                Note = request.Note
            };
            _context.MedicineGroups.Add(MedicineGroup);
            await _context.SaveChangesAsync();
            return MedicineGroup.IdMedicineGroup;
        }
        public async Task<long> Update(MedicineGroupUpdateRequest request)
        {
            var medicineGroup = await _context.MedicineGroups.FindAsync(request.IdMedicineGroup);
            if (medicineGroup == null)
                return 0;
            medicineGroup.MedicineGroupName = request.MedicineGroupName;
            medicineGroup.Note = request.Note;
            _context.MedicineGroups.Update(medicineGroup);
            await _context.SaveChangesAsync();
            return medicineGroup.IdMedicineGroup;
        }
        public async Task<long> Delete(long medicineGroupId)
        {
            var medicineGroup = await _context.MedicineGroups.FindAsync(medicineGroupId);
            if (medicineGroup == null) return 0;
            _context.MedicineGroups.Remove(medicineGroup);
            return await _context.SaveChangesAsync();
        }
    }
}
