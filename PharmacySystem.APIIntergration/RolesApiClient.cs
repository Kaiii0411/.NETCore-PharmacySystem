using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PharmacySystem.APIIntergration.Utilities;
using PharmacySystem.Models.Common;
using PharmacySystem.Models.Request;
using PharmacySystem.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.APIIntergration
{
    public interface IRolesApiClient
    {
        Task<ApiResult<List<RoleVM>>> GetAll();
        Task<PagedResult<RoleVM>> Get(GetRolesPagingRequest request);
        Task<ApiResult<bool>> Create(RoleCreateRequest request);
        Task<ApiResult<bool>> Update(Guid id, RoleUpdateRequest request);
        Task<ApiResult<RolesVM>> GetById(Guid id);
        Task<ApiResult<bool>> EditUsersInRoles(Guid id, UserAssignRequest request);
        Task<ApiResult<List<UsersRoleVM>>> GetUsersByRoles(Guid id);
    }
    public class RolesApiClient : IRolesApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public string _Address = SettingUrl.GetAddress();
        public RolesApiClient(IHttpClientFactory httpClientFactory,
           IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _httpClientFactory = httpClientFactory;
        }
        public async Task<ApiResult<List<RoleVM>>> GetAll()
        {
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_Address);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await client.GetAsync($"/api/roles/getall");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                List<RoleVM> myDeserializedObjList = (List<RoleVM>)JsonConvert.DeserializeObject(body, typeof(List<RoleVM>));
                return new ApiSuccessResult<List<RoleVM>>(myDeserializedObjList);
            }
            return JsonConvert.DeserializeObject<ApiErrorResult<List<RoleVM>>>(body);
        }
        public async Task<PagedResult<RoleVM>> Get(GetRolesPagingRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.BaseAddress = new Uri(_Address);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await client.GetAsync($"/api/roles/paging?RoleName={request.RoleName}");
            var body = await response.Content.ReadAsStringAsync();
            var roles = JsonConvert.DeserializeObject<PagedResult<RoleVM>>(body);
            return roles;
        }
        public async Task<ApiResult<bool>> Create(RoleCreateRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_Address);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/api/roles/create", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);

            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
        }
        public async Task<ApiResult<bool>> Update(Guid id, RoleUpdateRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_Address);
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"/api/roles/update/{id}", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);

            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
        }
        public async Task<ApiResult<RolesVM>> GetById(Guid id)
        {
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_Address);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await client.GetAsync($"/api/roles/details/{id}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<RolesVM>>(body);

            return JsonConvert.DeserializeObject<ApiErrorResult<RolesVM>>(body);
        }
        public async Task<ApiResult<bool>> EditUsersInRoles(Guid id, UserAssignRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_Address);
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"/api/roles/{id}/users", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);

            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
        }
        public async Task<ApiResult<List<UsersRoleVM>>> GetUsersByRoles(Guid id)
        {
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_Address);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await client.GetAsync($"/api/roles/users/{id}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                List<UsersRoleVM> myDeserializedObjList = (List<UsersRoleVM>)JsonConvert.DeserializeObject(body, typeof(List<UsersRoleVM>));
                return new ApiSuccessResult<List<UsersRoleVM>>(myDeserializedObjList);
            }
            return JsonConvert.DeserializeObject<ApiErrorResult<List<UsersRoleVM>>>(body);
        }
    }
}
