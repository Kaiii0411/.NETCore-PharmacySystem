using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using PharmacySystem.Models.Request;
using PharmacySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PharmacySystem.Models.Common;
using PharmacySystem.Models.ViewModels;
using PharmacySystem.Models.ReportModels;
using PharmacySystem.APIIntergration.Utilities;

namespace PharmacySystem.APIIntergration
{
    public interface IInvoiceApiClient
    {
        Task<int> CreateImportInvoice(ImportInvoiceCreateRequest request);
        Task<bool> DeleteImportInvoice(long id);
        Task<int> CreateExportInvoice(ExportInvoiceCreateRequest request);
        Task<bool> DeleteExportInvoice(long id);
        Task<PagedResult<ImportInvoiceVM>> GetImportInvoice(GetManageIInvoicePagingRequest request);
        Task<PagedResult<ExportInvoiceVM>> GetExportInvoice(GetManageEInvoicePagingRequest request);
        Task<PagedResult<ImportInvoiceVM>> GetImportInvoiceForSupplier(GetManageIInvoicePagingRequest request);
        Task<ImportInvoiceVM> GetImportInvoiceByID(long id);
        Task<ExportInvoiceVM> GetExportInvoiceByID(long id);
        Task<List<IInvoiceReportModels>> ProcGetImportInvoiceById(long id);
        Task<int> ProcessApproved(ProcessRequest request);
        Task<List<InvoiceDetailsDeleteVM>> GetDetailsByMedicineId(long id);
        Task<RevenueVM> GetRevenue();
        Task<int> ProcessReject(RejectRequest request);
    }
    public class InvoiceApiClient: BaseApiClient,IInvoiceApiClient
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public InvoiceApiClient(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor, IConfiguration configuration) : base(httpClientFactory, httpContextAccessor, configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }
        public async Task<int> CreateImportInvoice(ImportInvoiceCreateRequest request)
        {
            var body = await AddAsync<RequestResponse, ImportInvoiceCreateRequest>($"/api/importinvoices/create", request);
            return (int)body.StatusCode;
        }
        public async Task<int> CreateExportInvoice(ExportInvoiceCreateRequest request)
        {
            var body = await AddAsync<RequestResponse, ExportInvoiceCreateRequest>($"/api/exportinvoices/create", request);
            return (int)body.StatusCode;
        }
        public async Task<bool> DeleteImportInvoice(long id)
        {
            return await DeleteAsync($"api/importinvoices/delete/" + id);
        }
        public async Task<bool> DeleteExportInvoice(long id)
        {
            return await DeleteAsync($"api/exportinvoices/delete/" + id);
        }
        public async Task<PagedResult<ImportInvoiceVM>> GetImportInvoice(GetManageIInvoicePagingRequest request)
        {
            var data = await GetAsync<PagedResult<ImportInvoiceVM>>(
                $"/api/importinvoices/paging?DateCheckIn={request.DateCheckIn}&DateCheckOut={request.DateCheckOut}&StatusID={request.StatusID}&IdSupplier={request.IdSupplier}");
            return data;
        }
        public async Task<PagedResult<ImportInvoiceVM>> GetImportInvoiceForSupplier(GetManageIInvoicePagingRequest request)
        {
            var data = await GetAsync<PagedResult<ImportInvoiceVM>>(
                $"/api/importinvoices/pagingforsupplier?DateCheckIn={request.DateCheckIn}&DateCheckOut={request.DateCheckOut}&StatusID={request.StatusID}&IdSupplier={request.IdSupplier}");
            return data;
        }
        public async Task<PagedResult<ExportInvoiceVM>> GetExportInvoice(GetManageEInvoicePagingRequest request)
        {
            var data = await GetAsync<PagedResult<ExportInvoiceVM>>(
                $"/api/exportinvoices/paging?DateCheckIn={request.DateCheckIn}&DateCheckOut={request.DateCheckOut}&StatusID={request.StatusID}");
            return data;
        }
        public async Task<ImportInvoiceVM> GetImportInvoiceByID(long id)
        {
            var data = await GetAsync<ImportInvoiceVM>($"/api/importinvoices/details/{id}");
            return data;
        }
        public async Task<ExportInvoiceVM> GetExportInvoiceByID(long id)
        {
            var data = await GetAsync<ExportInvoiceVM>($"/api/exportinvoices/details/{id}");
            return data;
        }
        public async Task<int> ProcessApproved(ProcessRequest request)
        {
            var body = await PutAsync<RequestResponse, ProcessRequest>($"/api/importinvoices/process", request);
            return (int)body.StatusCode;
        }
        public async Task<int> ProcessReject(RejectRequest request)
        {
            var body = await PutAsync<RequestResponse, RejectRequest>($"/api/importinvoices/reject", request);
            return (int)body.StatusCode;
        }
        public async Task<List<IInvoiceReportModels>> ProcGetImportInvoiceById(long id)
        {
            var body = await GetAsync<RequestResponse>($"/api/importinvoices/procdetails/{id}");
            return OutPutApi.OutPut<IInvoiceReportModels>(body);
        }
        public async Task<List<InvoiceDetailsDeleteVM>> GetDetailsByMedicineId(long id)
        {
            var body = await GetAsync<RequestResponse>($"/api/invoicedetails/detailsbymedicine/{id}");
            return OutPutApi.OutPut<InvoiceDetailsDeleteVM>(body);
        }
        public async Task<RevenueVM> GetRevenue()
        {
            var data = await GetAsync<RevenueVM>($"/api/invoicedetails/revenue");
            return data;
        }
    }
}
