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
    public interface ISupplierApiClient
    {
        Task<List<Supplier>> GetListSupplier();
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
    }
}
