using Microsoft.EntityFrameworkCore;
using PharmacySystem.Models;
using PharmacySystem.Models.Common;
using PharmacySystem.Models.ReportModels;
using PharmacySystem.Models.Request;
using PharmacySystem.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.Service
{
    public interface IInvoiceService
    {
        Task<long> AddImportInvoice(ImportInvoiceCreateRequest request);
        Task<long> AddExportInvoice(ExportInvoiceCreateRequest request);
        Task<long> DeleteImportInvoice(long IdInvoice);
        Task<long> DeleteExportInvoice(long IdInvoice);
        Task<PagedResult<ImportInvoiceVM>> GetImportInvoice(GetManageIInvoicePagingRequest request);
        Task<PagedResult<ExportInvoiceVM>> GetExportInvoice(GetManageEInvoicePagingRequest request);
        Task<ImportInvoiceVM> GetImportInvoiceByID(long IdInvoice);
        Task<ExportInvoiceVM> GetExportInvoiceByID(long IdInvoice);
        Task<List<IInvoiceReportModels>> ProcGetImportInvoiceById(long IdInvoice);
    }
    public class InvoiceService : IInvoiceService
    {
        private readonly PharmacySystemContext _context;
        private readonly IInvoiceDetailsService _invoiceDetailsService;
        public InvoiceService(PharmacySystemContext context, IInvoiceDetailsService invoiceDetailsService)
        {
            this._context = context;
            this._invoiceDetailsService = invoiceDetailsService;
        }
        public async Task<long> AddImportInvoice(ImportInvoiceCreateRequest request)
        {
            ImportInvoice importInvoice = new ImportInvoice();
            importInvoice.IdAccount = request.IdAccount;
            importInvoice.DateCheckIn = request.DateCheckIn;
            importInvoice.DateCheckOut = request.DateCheckOut;
            importInvoice.StatusId = request.StatusID;
            importInvoice.Note = request.Note;
            importInvoice.IdSupplier = request.IdSupplier;
            _context.ImportInvoices.Add(importInvoice);
            await _context.SaveChangesAsync();

            foreach (var item in request.InvoiceDetails)
            {
                InvoiceDetail invoiceDetail = new InvoiceDetail();
                invoiceDetail.IdImportInvoice = importInvoice.IdImportInvoice;
                invoiceDetail.IdMedicine = item.MedicineId;
                invoiceDetail.Quantity = item.Quantity;
                invoiceDetail.TotalPrice = item.TotalPrice;
                _context.InvoiceDetails.Add(invoiceDetail);
            }
            await _context.SaveChangesAsync();

            return importInvoice.IdImportInvoice;
        }
        public async Task<long> AddExportInvoice(ExportInvoiceCreateRequest request)
        {
            ExportInvoice exportInvoice = new ExportInvoice();
            exportInvoice.IdAccount = request.IdAccount;
            exportInvoice.DateCheckIn = request.DateCheckIn;
            exportInvoice.DateCheckOut = request.DateCheckOut;
            exportInvoice.StatusId = request.StatusID;
            exportInvoice.Note = request.Note;
            _context.ExportInvoices.Add(exportInvoice);
            await _context.SaveChangesAsync();
            foreach (var item in request.InvoiceDetails)
            {
                InvoiceDetail invoiceDetail = new InvoiceDetail();
                invoiceDetail.IdExportInvoice = exportInvoice.IdExportInvoice;
                invoiceDetail.IdMedicine = item.MedicineId;
                invoiceDetail.Quantity = item.Quantity;
                invoiceDetail.TotalPrice = item.TotalPrice;
                _context.InvoiceDetails.Add(invoiceDetail);
            }
            await _context.SaveChangesAsync();
            return exportInvoice.IdExportInvoice;
        }
        public async Task<long> DeleteImportInvoice(long IdInvoice)
        {
            var invoice = await _context.ImportInvoices.FindAsync(IdInvoice);
            if (invoice == null) return 0;
            var invoiceDetails = await _context.InvoiceDetails.FindAsync(invoice.IdImportInvoice);
            _context.InvoiceDetails.Remove(invoiceDetails);
            _context.ImportInvoices.Remove(invoice);
            return await _context.SaveChangesAsync();
        }
        public async Task<long> DeleteExportInvoice(long IdInvoice)
        {
            var invoice = await _context.ExportInvoices.FindAsync(IdInvoice);
            if (invoice == null) return 0;
            var invoiceDetails = await _context.InvoiceDetails.FindAsync(invoice.IdExportInvoice);
            _context.InvoiceDetails.Remove(invoiceDetails);
            _context.ExportInvoices.Remove(invoice);
            return await _context.SaveChangesAsync();
        }
        public async Task<PagedResult<ImportInvoiceVM>> GetImportInvoice(GetManageIInvoicePagingRequest request)
        {
            var query = from i in _context.ImportInvoices
                        join id in _context.InvoiceDetails on i.IdImportInvoice equals id.IdImportInvoice
                        join a in _context.Users on i.IdAccount equals a.Id
                        join s in _context.Suppliers on i.IdSupplier equals s.IdSupplier
                        join st in _context.Statuses on i.StatusId equals st.StatusId
                        select new { i, id, s, st ,a};

            //search
            if (request.IdSupplier != null && request.IdSupplier != 0)
            {
                query = query.Where(x => x.s.IdSupplier == request.IdSupplier);
            }
            if (request.StatusID != null && request.StatusID != 0)
            {
                query = query.Where(x => x.i.StatusId == request.StatusID);
            }
            if(!string.IsNullOrEmpty(request.DateCheckIn.ToString()))
            {
                query = query.Where( x => x.i.DateCheckIn == request.DateCheckIn);
            }
            if (!string.IsNullOrEmpty(request.DateCheckOut.ToString()))
            {
                query = query.Where(x => x.i.DateCheckOut == request.DateCheckOut);
            }

            //list
            int totalRow = await query.CountAsync();
            var data = await query.Select(x => new ImportInvoiceVM()
            {
                IdImportInvoice = x.i.IdImportInvoice,
                UserName = x.a.UserName,
                DateCheckIn = x.i.DateCheckIn.GetValueOrDefault().ToString("yyyy-MM-dd"),
                DateCheckOut = x.i.DateCheckOut.GetValueOrDefault().ToString("yyyy-MM-dd"),
                StatusName = x.st.StatusName,
                StatusColor = x.st.StatusColor,
                StatusText = x.st.StatusText,
                Note = x.i.Note,
                Supplier = x.s.SupplierName,
            }).ToListAsync();

            //data
            var pagedResult = new PagedResult<ImportInvoiceVM>()
            {
                TotalRecords = totalRow,
                Items = data,
            };
            return pagedResult;
        }
        public async Task<PagedResult<ExportInvoiceVM>> GetExportInvoice(GetManageEInvoicePagingRequest request)
        {
            var query = from e in _context.ExportInvoices
                        join id in _context.InvoiceDetails on e.IdExportInvoice equals id.IdExportInvoice
                        join a in _context.Users on e.IdAccount equals a.Id
                        join st in _context.Statuses on e.StatusId equals st.StatusId
                        select new { e, id, st, a };

            //search
            if (request.StatusID != null && request.StatusID != 0)
            {
                query = query.Where(x => x.e.StatusId == request.StatusID);
            }
            if (!string.IsNullOrEmpty(request.DateCheckIn.ToString()))
            {
                query = query.Where(x => x.e.DateCheckIn == request.DateCheckIn);
            }
            if (!string.IsNullOrEmpty(request.DateCheckOut.ToString()))
            {
                query = query.Where(x => x.e.DateCheckOut == request.DateCheckOut);
            }

            //list
            int totalRow = await query.CountAsync();
            var data = await query.Select(x => new ExportInvoiceVM()
            {
                IdExportInvoice = x.e.IdExportInvoice,
                UserName = x.a.UserName,
                DateCheckIn = x.e.DateCheckIn.GetValueOrDefault().ToString("yyyy-MM-dd"),
                DateCheckOut = x.e.DateCheckOut.GetValueOrDefault().ToString("yyyy-MM-dd"),
                StatusName = x.st.StatusName,
                StatusColor = x.st.StatusColor,
                StatusText = x.st.StatusText,
                Note = x.e.Note
            }).ToListAsync();

            //data
            var pagedResult = new PagedResult<ExportInvoiceVM>()
            {
                TotalRecords = totalRow,
                Items = data,
            };
            return pagedResult;
        }
        public async Task<ImportInvoiceVM> GetImportInvoiceByID(long IdInvoice)
        {
            var query = from i in _context.ImportInvoices
                        join id in _context.InvoiceDetails on i.IdImportInvoice equals id.IdImportInvoice
                        join a in _context.Users on i.IdAccount equals a.Id
                        join s in _context.Suppliers on i.IdSupplier equals s.IdSupplier
                        join st in _context.Statuses on i.StatusId equals st.StatusId
                        select new { i, id, s, st, a };

            var listdetails = await _invoiceDetailsService.GetListImportDetails(IdInvoice);

            var invoiceDetails = await query.Where(x =>x.i.IdImportInvoice == IdInvoice).Select(invoice =>  new ImportInvoiceVM()
            {
                IdImportInvoice = invoice.i.IdImportInvoice,
                UserName = invoice.a.UserName,
                DateCheckIn = invoice.i.DateCheckIn.GetValueOrDefault().ToString("yyyy-MM-dd"),
                DateCheckOut = invoice.i.DateCheckOut.GetValueOrDefault().ToString("yyyy-MM-dd"),
                StatusName = invoice.st.StatusName,
                StatusColor = invoice.st.StatusColor,
                StatusText = invoice.st.StatusText,
                Note = invoice.i.Note,
                IdSupllier = invoice.s.IdSupplier,
                Supplier = invoice.s.SupplierName,
                invoiceDetails = listdetails
            }).FirstOrDefaultAsync();
            return invoiceDetails;
        }
        public async Task<ExportInvoiceVM> GetExportInvoiceByID(long IdInvoice)
        {
            var query = from i in _context.ExportInvoices
                        join id in _context.InvoiceDetails on i.IdExportInvoice equals id.IdExportInvoice
                        join a in _context.Users on i.IdAccount equals a.Id
                        join st in _context.Statuses on i.StatusId equals st.StatusId
                        select new { i, id, st, a };

            var listdetails = await _invoiceDetailsService.GetListExportDetails(IdInvoice);

            var invoiceDetails = await query.Where(x => x.i.IdExportInvoice == IdInvoice).Select(invoice => new ExportInvoiceVM()
            {
                IdExportInvoice = invoice.i.IdExportInvoice,
                UserName = invoice.a.UserName,
                DateCheckIn = invoice.i.DateCheckIn.GetValueOrDefault().ToString("yyyy-MM-dd"),
                DateCheckOut = invoice.i.DateCheckOut.GetValueOrDefault().ToString("yyyy-MM-dd"),
                StatusName = invoice.st.StatusName,
                StatusColor = invoice.st.StatusColor,
                StatusText = invoice.st.StatusText,
                Note = invoice.i.Note,
                invoiceDetails = listdetails
            }).FirstOrDefaultAsync();
            return invoiceDetails;
        }

        //store
        public async Task<List<IInvoiceReportModels>> ProcGetImportInvoiceById(long IdInvoice)
        {
            string StoreProc = "exec [dbo].[GetIInoviceDetails] " + "@id = " + IdInvoice;
            return await _context.IInvoiceReportModels.FromSqlRaw(StoreProc).ToListAsync();
        }
    }
}
