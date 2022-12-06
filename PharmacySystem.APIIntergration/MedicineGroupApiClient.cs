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
    public interface IMedicineGroupApiClient
    {
        Task<List<MedicineGroup>> GetListMedicineGroup();
        Task<int> CreateMedicineGroup(MedicineGroupCreateRequest request);
        Task<int> UpdateMedicineGroup(MedicineGroupUpdateRequest request);
        Task<bool> DeleteMedicineGroup(long id);
    }
    public class MedicineGroupApiClient: BaseApiClient, IMedicineGroupApiClient
    {
        public MedicineGroupApiClient(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor,
                    IConfiguration configuration) : base(httpClientFactory, httpContextAccessor, configuration)
        {   }
        public async Task<List<MedicineGroup>> GetListMedicineGroup()
        {
            var body = await GetAsync<RequestResponse>("api/medicinegroup/list");
            return OutPutApi.OutPut<MedicineGroup>(body);
        }
        public async Task<int> CreateMedicineGroup(MedicineGroupCreateRequest request)
        {
            var body = await AddAsync<RequestResponse, MedicineGroupCreateRequest>($"/api/medicinegroup/create", request);
            return (int)body.StatusCode;
        }
        public async Task<int> UpdateMedicineGroup(MedicineGroupUpdateRequest request)
        {
            var body = await PutAsync<RequestResponse, MedicineGroupUpdateRequest>($"/api/medicinegroup/update/" + request.IdMedicineGroup, request);
            return (int)body.StatusCode;
        }
        public async Task<bool> DeleteMedicineGroup(long id)
        {
            return await DeleteAsync($"api/medicinegroup/delete/" + id);
        }
    }
}
