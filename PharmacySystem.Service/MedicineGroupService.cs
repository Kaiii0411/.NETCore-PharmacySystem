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
    public interface IMedicineGroupService
    {
        Task<long> Create(MedicineGroupCreateRequest request);
        Task<long> Update(MedicineGroupUpdateRequest request);
        Task<long> Delete(long medicineGroupId);
        Task<IEnumerable<MedicineGroup>> GetListMedicineGroup();
    }
    public class MedicineGroupService : IMedicineGroupService
    {
        private readonly PharmacySystemContext _context;
        private readonly IMedicineGroupRepo _medicineGroupRepo;
        private readonly IMapper _mapper;

        public MedicineGroupService(PharmacySystemContext context, IMedicineGroupRepo medicineGroupRepo, IMapper mapper)
        {
            this._context = context;
            this._medicineGroupRepo = medicineGroupRepo;
            this._mapper = mapper;
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
        public async Task<IEnumerable<MedicineGroup>> GetListMedicineGroup()
        {
            IReadOnlyList<MedicineGroup> listMedicineGroup = await _medicineGroupRepo.ListAsync();
            return _mapper.Map<IEnumerable<MedicineGroup>>(listMedicineGroup);
        }
    }
}
