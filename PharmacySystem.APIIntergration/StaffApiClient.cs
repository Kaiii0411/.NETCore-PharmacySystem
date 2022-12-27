using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using PharmacySystem.APIIntergration.Utilities;
using PharmacySystem.Models.Common;
using PharmacySystem.Models.Request;
using PharmacySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PharmacySystem.Models.ViewModels;

namespace PharmacySystem.APIIntergration
{
    public interface IStaffApiClient
    {
        Task<int> CreateStaff(StaffCreateRequest request);
        Task<int> UpdateStaff(StaffUpdateRequest request);
        Task<PagedResult<StaffVM>> Get(GetManageStaffPagingRequest request);
        Task<staff> GetById(long id);
        Task<bool> DeleteStaff(long id);
        Task<List<staff>> GetListStaff();
    }
    public class StaffApiClient: BaseApiClient, IStaffApiClient
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public StaffApiClient(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor, IConfiguration configuration) : base(httpClientFactory, httpContextAccessor, configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }
        public async Task<int> CreateStaff(StaffCreateRequest request)
        {
            var body = await AddAsync<RequestResponse, StaffCreateRequest>($"/api/staff/create", request);
            return (int)body.StatusCode;
        }
        public async Task<int> UpdateStaff(StaffUpdateRequest request)
        {
            var body = await PutAsync<RequestResponse, StaffUpdateRequest>($"/api/staff/update", request);
            return (int)body.StatusCode;
        }
        public async Task<PagedResult<StaffVM>> Get(GetManageStaffPagingRequest request)
        {
            var data = await GetAsync<PagedResult<StaffVM>>(
                $"/api/staff/paging?StaffName={request.StaffName}&IdStaff={request.IdStaff}");
            return data;
        }
        public async Task<staff> GetById(long id)
        {
            var data = await GetAsync<staff>($"/api/staff/details/{id}");
            return data;
        }
        public async Task<bool> DeleteStaff(long id)
        {
            return await DeleteAsync($"api/staff/delete/" + id);
        }
        public async Task<List<staff>> GetListStaff()
        {
            var body = await GetAsync<RequestResponse>("api/staff/list");
            return OutPutApi.OutPut<staff>(body);
        }

    }
}
