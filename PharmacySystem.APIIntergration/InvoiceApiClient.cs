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
        Task<ImportInvoiceVM> GetImportInvoiceByID(long id);
        Task<ExportInvoiceVM> GetExportInvoiceByID(long id);
        Task<List<IInvoiceReportModels>> ProcGetImportInvoiceById(long id);
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
        public async Task<List<IInvoiceReportModels>> ProcGetImportInvoiceById(long id)
        {
            var body = await GetAsync<RequestResponse>($"/api/importinvoices/procdetails/{id}");
            return OutPutApi.OutPut<IInvoiceReportModels>(body);
        }
    }
}
