using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
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
        Task<List<MedicineVM>> GetListMedicineByMecicineGroup(long idMedicineGroup);
        Task<PagedResult<MedicineVM>> GetListOutOfDate(GetManageMedicinePagingRequest request);
        Task<PagedResult<MedicineVM>> GetListOutOfStock(GetManageMedicinePagingRequest request);
        Task<bool> ImportMedicineByExcel(IFormFile file);
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
            var medicine = await _context.Medicines.AsNoTracking().FirstOrDefaultAsync( x => x.IdMedicine == MedicineId);

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
        public async Task<List<MedicineVM>> GetListMedicineByMecicineGroup(long idMedicineGroup)
        {
            var list = new List<MedicineVM>();
            var query = from m in _context.Medicines
                        join s in _context.Suppliers on m.IdSupplier equals s.IdSupplier
                        select new { m, s };
            var listMedicne = await query.Where(x => x.m.IdMedicineGroup == idMedicineGroup).Select(x => new MedicineVM()
            {
                IdMedicine = x.m.IdMedicine,
                MedicineName = x.m.MedicineName,
                Description = x.m.Description,
                ExpiryDate = x.m.ExpiryDate,
                Quantity = x.m.Quantity,
                Unit = x.m.Unit,
                SellPrice = x.m.SellPrice,
                ImportPrice = x.m.ImportPrice,
                SupplierName = x.s.SupplierName
            }).ToListAsync();
            return listMedicne;
        }
        public async Task<PagedResult<MedicineVM>> GetListOutOfDate(GetManageMedicinePagingRequest request)
        {
            //select
            var query = from m in _context.Medicines
                        join mg in _context.MedicineGroups on m.IdMedicineGroup equals mg.IdMedicineGroup
                        join s in _context.Suppliers on m.IdSupplier equals s.IdSupplier
                        select new { m, mg, s };
            //search
            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.m.MedicineName.Contains(request.Keyword));
            if (request.IdMedicineGroup != null && request.IdMedicineGroup != 0)
            {
                query = query.Where(x => x.mg.IdMedicineGroup == request.IdMedicineGroup);
            }
            if (request.IdSupplier != null && request.IdSupplier != 0)
            {
                query = query.Where(x => x.s.IdSupplier == request.IdSupplier);
            }

            var ComingDate = DateTime.Now.Date.AddDays(10);

            //list
            var data = await query.Where(x => x.m.ExpiryDate.Date <= ComingDate).Select(x => new MedicineVM()
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

            int totalRow = await query.CountAsync();
            //data
            var pagedResult = new PagedResult<MedicineVM>()
            {
                TotalRecords = totalRow,
                Items = data
            };
            return pagedResult;
        }
        public async Task<PagedResult<MedicineVM>> GetListOutOfStock(GetManageMedicinePagingRequest request)
        {
            //select
            var query = from m in _context.Medicines
                        join mg in _context.MedicineGroups on m.IdMedicineGroup equals mg.IdMedicineGroup
                        join s in _context.Suppliers on m.IdSupplier equals s.IdSupplier
                        select new { m, mg, s };
            //search
            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.m.MedicineName.Contains(request.Keyword));
            if (request.IdMedicineGroup != null && request.IdMedicineGroup != 0)
            {
                query = query.Where(x => x.mg.IdMedicineGroup == request.IdMedicineGroup);
            }
            if (request.IdSupplier != null && request.IdSupplier != 0)
            {
                query = query.Where(x => x.s.IdSupplier == request.IdSupplier);
            }

            //list
            int totalRow = await query.CountAsync();
            var data = await query.Where(x => x.m.Quantity <= 20).Select(x => new MedicineVM()
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
        public async Task<bool> ImportMedicineByExcel(IFormFile file)
        {
            try
            {
                var list = new List<Medicine>();
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    using (var package = new ExcelPackage(stream))
                    {
                        ExcelWorksheet sheet = package.Workbook.Worksheets[0];
                        var rowcount = sheet.Dimension.Rows;
                        for (int row = 2; row <= rowcount; row++)
                        {
                            list.Add(new Medicine
                            {
                                MedicineName = sheet.Cells[row, 1].Value.ToString().Trim(),
                                IdMedicineGroup = Convert.ToInt64(sheet.Cells[row, 2].Value),
                                ExpiryDate = DateTime.Parse(sheet.Cells[row, 3].Value.ToString()),
                                Quantity = Convert.ToInt64(sheet.Cells[row, 4].Value),
                                Unit = sheet.Cells[row, 5].Value.ToString().Trim(),
                                SellPrice = Convert.ToDouble(sheet.Cells[row, 6].Value),
                                ImportPrice = Convert.ToDouble(sheet.Cells[row, 7].Value),
                                IdSupplier = Convert.ToInt64(sheet.Cells[row, 8].Value),
                            });
                        }
                        foreach(var item in list)
                        {
                            Medicine medicine = new Medicine();
                            medicine.MedicineName = item.MedicineName;
                            medicine.IdMedicineGroup = item.IdMedicineGroup;
                            medicine.ExpiryDate = item.ExpiryDate;
                            medicine.Quantity = item.Quantity;
                            medicine.Unit = item.Unit;
                            medicine.SellPrice = item.SellPrice;
                            medicine.ImportPrice = item.ImportPrice;
                            medicine.IdSupplier = item.IdSupplier;
                            _context.Medicines.Add(medicine);
                        }
                        await _context.SaveChangesAsync();
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
