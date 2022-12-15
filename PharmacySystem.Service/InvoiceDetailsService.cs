using Microsoft.EntityFrameworkCore;
using PharmacySystem.DataAccess.Repositorys;
using PharmacySystem.Models;
using PharmacySystem.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.Service
{
    public interface IInvoiceDetailsService
    {
        Task<List<InvoiceDetailsVM>> GetListImportDetails(long IdInvoice);
        Task<List<InvoiceDetailsVM>> GetListExportDetails(long IdInvoice);
    }
    public class InvoiceDetailsService : IInvoiceDetailsService
    {
        private readonly PharmacySystemContext _context;
        public InvoiceDetailsService(PharmacySystemContext context)
        {
            this._context = context;
        }
        public async Task<List<InvoiceDetailsVM>> GetListImportDetails(long IdInvoice)
        {
            var details = new List<InvoiceDetailsVM>();
            var query = from id in _context.InvoiceDetails
                        join m in _context.Medicines on id.IdMedicine equals m.IdMedicine
                              select new { id, m };

            var listdetails = await query.Where(x => x.id.IdImportInvoice == IdInvoice).Select( d => new InvoiceDetailsVM()
            {
                MedicineName = d.m.MedicineName,
                Quantity = d.id.Quantity,
                TotalPrice = d.id.TotalPrice
            }).ToListAsync();
            if (listdetails?.Any() == true)
            {
                foreach (var item in listdetails)
                {
                    details.Add(new InvoiceDetailsVM()
                    {
                        MedicineName = item.MedicineName,
                        Quantity = item.Quantity,
                        TotalPrice = item.TotalPrice
                    });
                }
            }
            return details;
        }
        public async Task<List<InvoiceDetailsVM>> GetListExportDetails(long IdInvoice)
        {
            var details = new List<InvoiceDetailsVM>();
            var query = from id in _context.InvoiceDetails
                        join m in _context.Medicines on id.IdMedicine equals m.IdMedicine
                        select new { id, m };

            var listdetails = await query.Where(x => x.id.IdExportInvoice == IdInvoice).Select(d => new InvoiceDetailsVM()
            {
                MedicineName = d.m.MedicineName,
                Quantity = d.id.Quantity,
                TotalPrice = d.id.TotalPrice
            }).ToListAsync();
            if (listdetails?.Any() == true)
            {
                foreach (var item in listdetails)
                {
                    details.Add(new InvoiceDetailsVM()
                    {
                        MedicineName = item.MedicineName,
                        Quantity = item.Quantity,
                        TotalPrice = item.TotalPrice
                    });
                }
            }
            return details;
        }

    }
}
