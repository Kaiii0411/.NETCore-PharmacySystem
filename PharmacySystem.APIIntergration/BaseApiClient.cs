using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PharmacySystem.APIIntergration.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PharmacySystem.APIIntergration
{
    public class BaseApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public string _Address = SettingUrl.GetAddress();

        protected BaseApiClient(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _httpClientFactory = httpClientFactory;
        }
        protected async Task<T> GetAsync<T>(string url)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_Address);
            HttpResponseMessage response;
            string body;
            response = await client.GetAsync(url);
            body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                T myDeserializedObjList = (T)JsonConvert.DeserializeObject(body, typeof(T));
                return myDeserializedObjList;
            }
            return JsonConvert.DeserializeObject<T>(body);
        }
        public async Task<List<T>> GetListAsync<T>(string url, bool requiredLogin = false)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(url);
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var data = (List<T>)JsonConvert.DeserializeObject(body, typeof(List<T>));
                return data;
            }
            throw new Exception(body);
        }
        protected async Task<TResponse> AddAsync<TResponse, T>(string url, T data)
        {
            string json = JsonConvert.SerializeObject(data);
            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_Address);
            HttpResponseMessage response;
            string body;
            try
            {
                response = await client.PostAsync(url, httpContent);
            }
            catch (Exception)
            {
                Object bodyOB = new { ErrorCode = 1, Content = "" };
                body = JsonConvert.SerializeObject(bodyOB);
                return JsonConvert.DeserializeObject<TResponse>(body);
            }

            body = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                TResponse myDeserializedObjList = (TResponse)JsonConvert.DeserializeObject(body,
                typeof(TResponse));

                return myDeserializedObjList;
            }
            return JsonConvert.DeserializeObject<TResponse>(body);
        }
        protected async Task<TResponse> PutAsync<TResponse, T>(string url, T data)
        {
            string json = JsonConvert.SerializeObject(data);
            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_Address);
            HttpResponseMessage response;
            string body;
            try
            {
                response = await client.PutAsync(url, httpContent);
            }
            catch (Exception)
            {
                Object bodyOB = new { ErrorCode = 1, Content = "" };
                body = JsonConvert.SerializeObject(bodyOB);
                return JsonConvert.DeserializeObject<TResponse>(body);
            }

            body = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                TResponse myDeserializedObjList = (TResponse)JsonConvert.DeserializeObject(body,
                typeof(TResponse));

                return myDeserializedObjList;
            }
            return JsonConvert.DeserializeObject<TResponse>(body);
        }
    }
}
