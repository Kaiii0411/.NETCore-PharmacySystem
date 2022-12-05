using PharmacySystem.Models;
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
    }
    public class InvoiceService : IInvoiceService
    {
        private readonly PharmacySystemContext _context;
        public InvoiceService(PharmacySystemContext context)
        {
            this._context = context;
        }
        public async Task<long> AddImportInvoice(ImportInvoiceCreateRequest request)
        {
            ImportInvoice importInvoice = new ImportInvoice();
            importInvoice.IdAccount = request.IdAccount;
            importInvoice.DateCheckIn = request.DateCheckIn;
            importInvoice.DateCheckOut = request.DateCheckOut;
            importInvoice.Status.StatusId = request.StatusID;
            importInvoice.Note = request.Note;
            importInvoice.IdSupplier = request.IdSupplier;
            _context.ImportInvoices.Add(importInvoice);
            await _context.SaveChangesAsync();
            List<IInvoice> invoiceItems = new List<IInvoice>();
            foreach (var item in invoiceItems)
            {
                InvoiceDetail invoiceDetail = new InvoiceDetail();
                invoiceDetail.IdImportInvoice = importInvoice.IdImportInvoice;
                invoiceDetail.IdMedicine = item.IIdMedicine;
                invoiceDetail.Quantity = item.IQuantity;
                invoiceDetail.TotalPrice = item.ITotalPrice;
                _context.InvoiceDetails.Add(invoiceDetail);
            }
            await _context.SaveChangesAsync();
            return importInvoice.IdImportInvoice;
        }
        public async Task<long> AddExportInvoice(ExportInvoiceCreateRequest request)
        {
            ImportInvoice importInvoice = new ImportInvoice();
            importInvoice.IdAccount = request.IdAccount;
            importInvoice.DateCheckIn = request.DateCheckIn;
            importInvoice.DateCheckOut = request.DateCheckOut;
            importInvoice.Status.StatusId = request.StatusID;
            importInvoice.Note = request.Note;
            _context.ImportInvoices.Add(importInvoice);
            await _context.SaveChangesAsync();
            List<IInvoice> invoiceItems = new List<IInvoice>();
            foreach (var item in invoiceItems)
            {
                InvoiceDetail invoiceDetail = new InvoiceDetail();
                invoiceDetail.IdImportInvoice = importInvoice.IdImportInvoice;
                invoiceDetail.IdMedicine = item.IIdMedicine;
                invoiceDetail.Quantity = item.IQuantity;
                invoiceDetail.TotalPrice = item.ITotalPrice;
                _context.InvoiceDetails.Add(invoiceDetail);
            }
            await _context.SaveChangesAsync();
            return importInvoice.IdImportInvoice;
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
    }
}
