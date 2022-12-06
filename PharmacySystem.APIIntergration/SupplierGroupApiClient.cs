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
    public interface ISupplierGroupApiClient
    {
        Task<int> CreateSupplierGroup(SupplierGroupCreateRequest request);
        Task<int> UpdateSupplierGroup(SupplierGroupUpdateRequest request);
        Task<bool> DeleteSupplierGroup(long id);
    }
    public class SupplierGroupApiClient: BaseApiClient, ISupplierGroupApiClient
    {
        public SupplierGroupApiClient(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration) : base(httpClientFactory, httpContextAccessor, configuration)
        { }
        public async Task<List<SupplierGroup>> GetListSupplier()
        {
            var body = await GetAsync<RequestResponse>("api/suppliergroup/list");
            return OutPutApi.OutPut<SupplierGroup>(body);
        }
        public async Task<int> CreateSupplierGroup(SupplierGroupCreateRequest request)
        {
            var body = await AddAsync<RequestResponse, SupplierGroupCreateRequest>($"/api/suppliergroup/create", request);
            return (int)body.StatusCode;
        }
        public async Task<int> UpdateSupplierGroup(SupplierGroupUpdateRequest request)
        {
            var body = await PutAsync<RequestResponse, SupplierGroupUpdateRequest>($"/api/suppliergroup/update/" + request.IdSupplierGroup, request);
            return (int)body.StatusCode;
        }
        public async Task<bool> DeleteSupplierGroup(long id)
        {
            return await DeleteAsync($"api/suppliergroup/delete/" + id);
        }
    }
}
