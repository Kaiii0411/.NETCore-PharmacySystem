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
using PharmacySystem.APIIntergration.Utilities;

namespace PharmacySystem.APIIntergration
{
    public interface IStoreApiClient
    {
        Task<int> CreateStore(StoreCreateRequest request);
        Task<int> UpdateStore(StoreUpdateRequest request);
        Task<PagedResult<Store>> Get(GetManageStorePagingRequest request);
        Task<Store> GetById(long id);
        Task<bool> DeleteStore(long id);
        Task<List<Store>> GetListStore();
    }
    public class StoreApiClient : BaseApiClient, IStoreApiClient
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public StoreApiClient(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor, IConfiguration configuration) : base(httpClientFactory, httpContextAccessor, configuration)
        {
            this._httpContextAccessor = httpContextAccessor;
            this._configuration = configuration;
            this._httpClientFactory = httpClientFactory;
        }
        public async Task<int> CreateStore(StoreCreateRequest request)
        {
            var body = await AddAsync<RequestResponse, StoreCreateRequest>($"/api/store/create", request);
            return (int)body.StatusCode;
        }
        public async Task<int> UpdateStore(StoreUpdateRequest request)
        {
            var body = await PutAsync<RequestResponse, StoreUpdateRequest>($"/api/store/update", request);
            return (int)body.StatusCode;
        }
        public async Task<PagedResult<Store>> Get(GetManageStorePagingRequest request)
        {
            var data = await GetAsync<PagedResult<Store>>(
                $"/api/medicines/paging?StoreName={request.StoreName}&IdStore={request.IdStore}");
            return data;
        }
        public async Task<Store> GetById(long id)
        {
            var data = await GetAsync<Store>($"/api/store/details/{id}");
            return data;
        }
        public async Task<bool> DeleteStore(long id)
        {
            return await DeleteAsync($"api/store/delete/" + id);
        }
        public async Task<List<Store>> GetListStore()
        {
            var body = await GetAsync<RequestResponse>("api/store/list");
            return OutPutApi.OutPut<Store>(body);
        }
    }
}
