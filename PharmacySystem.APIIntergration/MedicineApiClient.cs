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
    public interface IMedicineApiClient
    {
        Task<int> CreateMedicine(MedicineCreateRequest request);
        Task<int> UpdateMedicine(MedicineUpdateRequest request);
        Task<PagedResult<MedicineVM>> Get(GetManageMedicinePagingRequest request);
        Task<Medicine> GetById(long id);
        Task<bool> DeleteMedicine(long id);
        Task<List<Medicine>> GetListMedicine();
        Task<List<MedicineVM>> GetListByMedicineGroupId(long id);
        Task<PagedResult<MedicineVM>> GetListOutOfStock(GetManageMedicinePagingRequest request);
        Task<PagedResult<MedicineVM>> GetListOutOfDate(GetManageMedicinePagingRequest request);
        Task<bool> ImportExcel(IFormFile file);
    }
    public class MedicineApiClient: BaseApiClient, IMedicineApiClient
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public MedicineApiClient(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor, IConfiguration configuration): base(httpClientFactory, httpContextAccessor, configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }
        public async Task<int> CreateMedicine(MedicineCreateRequest request)
        {
            var body = await AddAsync<RequestResponse, MedicineCreateRequest>($"/api/medicines/create", request);
            return (int)body.StatusCode;
        }
        public async Task<int> UpdateMedicine(MedicineUpdateRequest request)
        {
            var body = await PutAsync<RequestResponse, MedicineUpdateRequest>($"/api/medicines/update", request);
            return (int)body.StatusCode;
        }
        public async Task<PagedResult<MedicineVM>> Get(GetManageMedicinePagingRequest request)
        {
            var data = await GetAsync<PagedResult<MedicineVM>>(
                $"/api/medicines/paging?Keyword={request.Keyword}&IdMedicineGroup={request.IdMedicineGroup}&IdSupplier={request.IdSupplier}");
            return data;
        }
        public async Task<Medicine> GetById(long id)
        {
            var data = await GetAsync<Medicine>($"/api/medicines/details/{id}");
            return data;
        }
        public async Task<bool> DeleteMedicine(long id)
        {
            return await DeleteAsync($"api/medicines/delete/" + id);
        }
        public async Task<List<Medicine>> GetListMedicine()
        {
            var body = await GetAsync<RequestResponse>("api/medicines/list");
            return OutPutApi.OutPut<Medicine>(body);
        }
        public async Task<List<MedicineVM>> GetListByMedicineGroupId(long id)
        {
            var body = await GetAsync<RequestResponse>($"/api/medicines/listbymedicinegroupid/{id}");
            return OutPutApi.OutPut<MedicineVM>(body);
        }
        public async Task<PagedResult<MedicineVM>> GetListOutOfStock(GetManageMedicinePagingRequest request)
        {
            var data = await GetAsync<PagedResult<MedicineVM>>(
                $"/api/medicines/paging/outstock?Keyword={request.Keyword}&IdMedicineGroup={request.IdMedicineGroup}&IdSupplier={request.IdSupplier}");
            return data;
        }
        public async Task<PagedResult<MedicineVM>> GetListOutOfDate(GetManageMedicinePagingRequest request)
        {
            var data = await GetAsync<PagedResult<MedicineVM>>(
                $"/api/medicines/paging/outdate?Keyword={request.Keyword}&IdMedicineGroup={request.IdMedicineGroup}&IdSupplier={request.IdSupplier}");
            return data;
        }
        public async Task<bool> ImportExcel(IFormFile file)
        {
            return await AddFileAsync($"api/medicines/import", file);
        }
    }
}
