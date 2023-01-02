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
        Task<List<InvoiceDetailsDeleteVM>> GetListDeatilsByIdMedicine(long IdMedicine);
        Task<RevenueVM> GetRevenue();
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
        public async Task<List<InvoiceDetailsDeleteVM>> GetListDeatilsByIdMedicine(long IdMedicine)
        {
            var details = new List<InvoiceDetailsDeleteVM>();
            var query = from id in _context.InvoiceDetails
                        select id;
            var listdeatils = await query.Where(x => x.IdMedicine == IdMedicine).Select(d => new InvoiceDetailsDeleteVM()
            {
                IdImportInvoice = d.IdImportInvoice,
                IdExportInvoice = d.IdExportInvoice,
                Quantity = d.Quantity,
                TotalPrice = d.TotalPrice
            }).ToListAsync();
            if(listdeatils.Any() == true)
            {
                foreach (var item in listdeatils)
                {
                    details.Add(new InvoiceDetailsDeleteVM()
                    {
                        IdExportInvoice = item.IdExportInvoice,
                        IdImportInvoice = item.IdImportInvoice,
                        Quantity = item.Quantity,
                        TotalPrice = item.TotalPrice
                    });
                }
            }
            return details;
        }
        public async Task<RevenueVM> GetRevenue()
        {
            var query = from e in _context.ExportInvoices
                        join id in _context.InvoiceDetails on e.IdExportInvoice equals id.IdExportInvoice
                        select new { e, id };
            var listInvoiceToday = await query.Where(x => x.e.DateCheckOut.Value.Date == DateTime.Now.Date).Select(x => x.id.TotalPrice).ToArrayAsync();
            var listInvoiceYesterday = await query.Where(x => x.e.DateCheckOut.Value.Date == DateTime.Now.Date.AddDays(-1)).Select(x => x.id.TotalPrice).ToArrayAsync();
            var ListCount = listInvoiceToday.Count();
            //declare
            double TotalPriceYesterday = listInvoiceYesterday.Sum();
            double TotalPriceToday = listInvoiceToday.Sum();
            var Pecent = (TotalPriceToday - TotalPriceYesterday) / TotalPriceYesterday * 100;
            return new RevenueVM()
            {
                TotalPriceYesterday = TotalPriceYesterday,
                TotalPriceNow = TotalPriceToday,
                InvoiceTotal = ListCount,
                PercentDifference = Math.Round(Pecent, 2)
            };
        }
    }
}
