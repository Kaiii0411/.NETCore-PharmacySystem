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
    public interface IMedicineService
    {
        Task<long> Create(MedicineCreateRequest request);
        Task<long> Update(MedicineUpdateRequest request);
        Task<long> Delete(long medicineId);
        Task<PagedResult<MedicineVM>> Get(GetManageMedicinePagingRequest request);
        Task<Medicine> GetByID(long MedicineId);
        Task<IEnumerable<Medicine>> GetListMedicine();
    }
    public class MedicineService : IMedicineService
    {
        private readonly PharmacySystemContext _context;
        private readonly IMedicineRepo _medicineRepo;
        private readonly IMapper _mapper;

        //crud
        public MedicineService(PharmacySystemContext context, IMedicineRepo medicineRepo, IMapper mapper)
        {
            this._context = context;
            this._medicineRepo = medicineRepo;
            this._mapper = mapper;
        }
        public async Task<long> Create(MedicineCreateRequest request)
        {
            var Medicine = new Medicine()
            {
                MedicineName = request.MedicineName,
                Description = request.Description,
                IdMedicineGroup = request.IdMedicineGroup,
                ExpiryDate = request.ExpiryDate,
                Quantity = request.Quantity,
                Unit = request.Unit,
                SellPrice = request.SellPrice,
                ImportPrice = request.ImportPrice,
                IdSupplier = request.IdSupplier
            };
            _context.Medicines.Add(Medicine);
            await _context.SaveChangesAsync();
            return Medicine.IdMedicine;
        }
        public async Task<long> Update(MedicineUpdateRequest request)
        {
            var medicine = await _context.Medicines.FindAsync(request.IdMedicine);
            if (medicine == null)
                return 0;
            medicine.MedicineName = request.MedicineName;
            medicine.Description = request.Description;
            medicine.IdMedicineGroup = request.IdMedicineGroup;
            medicine.ExpiryDate = request.ExpiryDate;
            medicine.Quantity = request.Quantity;
            medicine.Unit = request.Unit;
            medicine.SellPrice = request.SellPrice;
            medicine.ImportPrice = request.ImportPrice;
            medicine.IdSupplier = request.IdSupplier;
            _context.Medicines.Update(medicine);
            await _context.SaveChangesAsync();
            return medicine.IdMedicine;

        }
        public async Task<long> Delete(long medicineId)
        {
            var medicine = await _context.Medicines.FindAsync(medicineId);
            if (medicine == null) return 0;
            _context.Medicines.Remove(medicine);
            return await _context.SaveChangesAsync();
        }
        public async Task<PagedResult<MedicineVM>> Get(GetManageMedicinePagingRequest request)
        {
            //select
            var query = from m in _context.Medicines 
                       join mg in _context.MedicineGroups on m.IdMedicineGroup equals mg.IdMedicineGroup
                       join s in _context.Suppliers on m.IdSupplier equals s.IdSupplier
                       select new { m, mg, s};
            //search
            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.m.MedicineName.Contains(request.Keyword));
            if(request.IdMedicineGroup != null && request.IdMedicineGroup !=0)
            {
                query = query.Where(x => x.mg.IdMedicineGroup == request.IdMedicineGroup);
            }
            if (request.IdSupplier != null && request.IdSupplier != 0)
            {
                query = query.Where(x => x.s.IdSupplier == request.IdSupplier);
            }
            //list
            int totalRow = await query.CountAsync();
            var data = await query.Select(x => new MedicineVM()
            {
                IdMedicine = x.m.IdMedicine,
                MedicineName = x.m.MedicineName,
                Description = x.m.Description,
                MedicineGroupName = x.mg.MedicineGroupName,
                ExpiryDate = x.m.ExpiryDate,
                Quantity = x.m.Quantity,
                Unit = x.m.Unit,
                SellPrice = x.m.SellPrice,
                ImportPrice = x.m.ImportPrice,
                SupplierName = x.s.SupplierName
            }).ToListAsync();
            //data
            var pagedResult = new PagedResult<MedicineVM>()
            {
                TotalRecords = totalRow,
                Items = data
            };
            return pagedResult;
        }
        public async Task<Medicine> GetByID(long MedicineId)
        {
            var medicine = await _context.Medicines.FindAsync(MedicineId);

            var medicineDetails = new Medicine()
            {
                IdMedicine = medicine.IdMedicine,
                MedicineName = medicine.MedicineName,
                Description = medicine.Description,
                IdMedicineGroup = medicine.IdMedicineGroup,
                ExpiryDate = medicine.ExpiryDate,
                Quantity = medicine.Quantity,
                Unit = medicine.Unit,
                SellPrice = medicine.SellPrice,
                ImportPrice = medicine.ImportPrice,
                IdSupplier = medicine.IdSupplier
            };
            return medicineDetails;
        }
        public async Task<IEnumerable<Medicine>> GetListMedicine()
        {
            IReadOnlyList<Medicine> listMedicineGroup = await _medicineRepo.ListAsync();
            return _mapper.Map<IEnumerable<Medicine>>(listMedicineGroup);
        }
    }
}
