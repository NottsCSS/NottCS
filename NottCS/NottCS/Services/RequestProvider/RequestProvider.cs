using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using NottCS.Models.Request;
using Newtonsoft.Json;

namespace NottCS.Services.RequestProvider
{
    internal class RequestProvider : IRequestProvider
    {
        private HttpClient client = new HttpClient();

        public async Task<RequestResult<TData>> DeleteData<TData>(string url, string token)
        {
            if (string.IsNullOrEmpty(url))
                throw new Exception("Empty url provided");
            var request = new HttpRequestMessage(HttpMethod.Delete, url);
            var requestTask = client.SendAsync(request);

            var httpResponse = await requestTask;

            return new RequestResult<TData>()
            {
                IsSuccess = httpResponse.IsSuccessStatusCode,
                Message = httpResponse.ReasonPhrase,
                Result = httpResponse.IsSuccessStatusCode ? JsonConvert.DeserializeObject<ServerResult<TData>>(await httpResponse.Content.ReadAsStringAsync()) : null
            };

        }

        public async Task<RequestResult<TData>> GetDataAsync<TData>(string url, string token)
        {
            if (string.IsNullOrEmpty(url))
                throw new Exception("Empty url provided");

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var requestTask = client.SendAsync(request);

            var httpResponse = await requestTask;

            return new RequestResult<TData>()
            {
                IsSuccess = httpResponse.IsSuccessStatusCode,
                Message = httpResponse.ReasonPhrase,
                Result = httpResponse.IsSuccessStatusCode ? JsonConvert.DeserializeObject<ServerResult<TData>>(await httpResponse.Content.ReadAsStringAsync()) : null
            };
        }

        public async Task<RequestResult<TData>> PostDataAsync<TData>(string url, TData requestBody, string token)
        {
            if (string.IsNullOrEmpty(url))
                throw new Exception("Empty url provided");

            var request = new HttpRequestMessage(HttpMethod.Post, url);

            //Add request body
            var requestBodyJson = JsonConvert.SerializeObject(requestBody);
            request.Content = new StringContent(requestBodyJson, Encoding.UTF8, "application/json");

            var requestTask = client.SendAsync(request);

            var httpResponse = await requestTask;

            return new RequestResult<TData>()
            {
                IsSuccess = httpResponse.IsSuccessStatusCode,
                Message = httpResponse.ReasonPhrase,
                Result = httpResponse.IsSuccessStatusCode ? JsonConvert.DeserializeObject<ServerResult<TData>>(await httpResponse.Content.ReadAsStringAsync()) : null
            };
        }

        public async Task<RequestResult<TData>> PutDataAsync<TData>(string url, TData requestBody, string token)
        {
            if (string.IsNullOrEmpty(url))
                throw new Exception("Empty url provided");

            var request = new HttpRequestMessage(HttpMethod.Put, url);

            //Add request body
            var requestBodyJson = JsonConvert.SerializeObject(requestBody);
            request.Content = new StringContent(requestBodyJson, Encoding.UTF8, "application/json");

            var requestTask = client.SendAsync(request);

            var httpResponse = await requestTask;

            return new RequestResult<TData>()
            {
                IsSuccess = httpResponse.IsSuccessStatusCode,
                Message = httpResponse.ReasonPhrase,
                Result = httpResponse.IsSuccessStatusCode ? JsonConvert.DeserializeObject<ServerResult<TData>>(await httpResponse.Content.ReadAsStringAsync()) : null
            };
        }
    }
}
