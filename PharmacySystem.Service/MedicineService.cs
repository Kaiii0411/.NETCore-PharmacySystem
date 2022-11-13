using PharmacySystem.Models;
using PharmacySystem.Models.Request;
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
    }
    public class MedicineService : IMedicineService
    {
        private readonly PharmacySystemContext _context;
        public MedicineService(PharmacySystemContext context)
        {
            this._context = context;
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
    }
}
