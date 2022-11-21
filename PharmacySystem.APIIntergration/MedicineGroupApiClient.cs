using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using PharmacySystem.APIIntergration.Utilities;
using PharmacySystem.Models;
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
    }
    public class MedicineGroupApiClient: BaseApiClient, IMedicineGroupApiClient
    {
        public MedicineGroupApiClient(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor,
                    IConfiguration configuration) : base(httpClientFactory, httpContextAccessor, configuration)
        {   }
        public async Task<List<MedicineGroup>> GetListMedicineGroup()
        {
            var body = await GetAsync<RequestResponse>("api/medicinegroup");
            return OutPutApi.OutPut<MedicineGroup>(body);
        }
    }
}
