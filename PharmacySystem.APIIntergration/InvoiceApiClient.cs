using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using PharmacySystem.Models.Request;
using PharmacySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.APIIntergration
{
    public interface IInvoiceApiClient
    {
        Task<int> CreateImportInvoice(ImportInvoiceCreateRequest request);
        Task<bool> DeleteImportInvoice(long id);
        Task<int> CreateExportInvoice(ExportInvoiceCreateRequest request);
        Task<bool> DeleteExportInvoice(long id);
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
        public async Task<bool> DeleteImportInvoice(long id)
        {
            return await DeleteAsync($"api/importinvoices/delete/" + id);
        }
        public async Task<int> CreateExportInvoice(ExportInvoiceCreateRequest request)
        {
            var body = await AddAsync<RequestResponse, ExportInvoiceCreateRequest>($"/api/exportinvoices/create", request);
            return (int)body.StatusCode;
        }
        public async Task<bool> DeleteExportInvoice(long id)
        {
            return await DeleteAsync($"api/exportinvoices/delete/" + id);
        }
    }
}
