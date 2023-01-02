using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using PharmacySystem.APIIntergration.Utilities;
using PharmacySystem.Models;
using PharmacySystem.Models.Common;
using PharmacySystem.Models.Request;
using PharmacySystem.Models.ViewModels;
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
        Task<PagedResult<SupplierVM>> Get(GetManageSupplierPagingRequest request);
        Task<Supplier> GetById(long id);
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
            var body = await PutAsync<RequestResponse, SupplierUpdateRequest>($"/api/supplier/update", request);
            return (int)body.StatusCode;
        }
        public async Task<bool> DeleteSupplier(long id)
        {
            return await DeleteAsync($"api/supplier/delete/" + id);
        }
        public async Task<PagedResult<SupplierVM>> Get(GetManageSupplierPagingRequest request)
        {
            var data = await GetAsync<PagedResult<SupplierVM>>(
                $"/api/supplier/paging?Keyword={request.Keyword}&IdSupplierGroup={request.IdSupplierGroup}");
            return data;
        }
        public async Task<Supplier> GetById(long id)
        {
            var data = await GetAsync<Supplier>($"/api/supplier/details/{id}");
            return data;
        }
        public async Task<List<SupplierVM>> GetListBuSupplierGroup(long id)
        {
            var body = await GetAsync<RequestResponse>($"/api/supplier/listbysuppliergroupid/{id}");
            return OutPutApi.OutPut<SupplierVM>(body);
        }
    }
}
