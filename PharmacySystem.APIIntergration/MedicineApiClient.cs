using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using PharmacySystem.APIIntergration.Utilities;
using PharmacySystem.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.APIIntergration
{
    public interface IMedicineApiClient
    {
        Task<bool> CreateMedicine(MedicineCreateRequest request);
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
        public async Task<bool> CreateMedicine(MedicineCreateRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_Address);
            var requestContent = new MultipartFormDataContent();
            requestContent.Add(new StringContent(String.IsNullOrEmpty(request.MedicineName) ? "" : request.MedicineName.ToString()), "MedicineName");
            requestContent.Add(new StringContent(String.IsNullOrEmpty(request.Description) ? "" : request.Description.ToString()), "Description");
            requestContent.Add(new StringContent(request.IdMedicineGroup.ToString()), "IdMedicineGroup");
            requestContent.Add(new StringContent(request.ExpiryDate.ToString()), "ExpiryDate");
            requestContent.Add(new StringContent(request.Quantity.ToString()), "Quantity");
            requestContent.Add(new StringContent(String.IsNullOrEmpty(request.Unit) ? "" : request.Unit.ToString()), "Unit");
            requestContent.Add(new StringContent(request.SellPrice.ToString()), "SellPrice");
            requestContent.Add(new StringContent(request.ImportPrice.ToString()), "ImportPrice");
            requestContent.Add(new StringContent(request.IdSupplier.ToString()), "IdSupplier");
            var response = await client.PostAsync($"/api/medicines/", requestContent);
            return response.IsSuccessStatusCode;
        }
    }
}
