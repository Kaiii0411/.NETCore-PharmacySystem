using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using PharmacySystem.APIIntergration.Utilities;
using PharmacySystem.Models;
using PharmacySystem.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.APIIntergration
{
    public interface ISupplierApiClient
    {
        Task<List<Supplier>> GetListSupplier();
        Task<int> CreateSupplier(SupplierCreateRequest request);
        Task<int> UpdateSupplier(SupplierUpdateRequest request);
        Task<bool> DeleteSupplier(long id);
    }
    public class SupplierApiClient : BaseApiClient, ISupplierApiClient
    {
        public SupplierApiClient(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration) : base(httpClientFactory, httpContextAccessor, configuration)
        { }
        public async Task<List<Supplier>> GetListSupplier()
        {
            var body = await GetAsync<RequestResponse>("api/supplier/list");
            return OutPutApi.OutPut<Supplier>(body);
        }
        public async Task<int> CreateSupplier(SupplierCreateRequest request)
        {
            var body = await AddAsync<RequestResponse, SupplierCreateRequest>($"/api/supplier/create", request);
            return (int)body.StatusCode;
        }
        public async Task<int> UpdateSupplier(SupplierUpdateRequest request)
        {
            var body = await PutAsync<RequestResponse, SupplierUpdateRequest>($"/api/supplier/update/" + request.IdSupplier, request);
            return (int)body.StatusCode;
        }
        public async Task<bool> DeleteSupplier(long id)
        {
            return await DeleteAsync($"api/supplier/delete/" + id);
        }
    }
}
