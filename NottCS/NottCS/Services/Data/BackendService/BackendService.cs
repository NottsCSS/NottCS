using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NottCS.Models;
using NottCS.PrivateData;

namespace NottCS.Services.BackendService
{
    //TODO: Check validity of token and refresh it every time trying to connect to server
    public class BackendService
    {
        private readonly HttpClient _client;
        private bool _isClientSetup = false;
        private readonly string _baseAddress = Config.EndpointAddress;
        private readonly ILogger<BackendService> _logger;

        public BackendService(ILogger<BackendService> logger)
        {
            _logger = logger;
            _client = new HttpClient()
            {
                BaseAddress = new Uri(_baseAddress),
                Timeout = new TimeSpan(0,0,15)
            };
        }
        
        public void SetupClient(string accessToken)
        {
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            _isClientSetup = true;
        }
        public void ResetClient()
        {
            _client.DefaultRequestHeaders.Clear();
            _isClientSetup = false;
        }

        private HttpRequestMessage HttpRequestMessageGenerator(HttpMethod httpMethod, string requestUri, object requestBody = null)
        {
            #region ObjectValidator
            if (httpMethod == HttpMethod.Post && requestBody == null)
            {
                throw new Exception("No valid request body");
            }
            #endregion

            var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
            HttpRequestMessage httpRequest;
            if (httpMethod == HttpMethod.Get)
            {
                httpRequest = new HttpRequestMessage(httpMethod, requestUri);
            }
            else
            {
                httpRequest = new HttpRequestMessage(httpMethod, requestUri)
                {
                    Content = content
                };
            }
            return httpRequest;


        }
        /// <summary>
        /// Generates proper request Uri based on the method
        /// </summary>
        /// <typeparam name="T">Type of modal class requested</typeparam>
        /// <param name="httpMethod">Request HttpMethod</param>
        /// <param name="identifier">Identifier for the server to lookup data</param>
        /// <returns></returns>
        private string UriGenerator<T>(HttpMethod httpMethod, string identifier = null)
        {
            string returnUri;
            if (typeof(T) == typeof(Models.Club))
            {
                returnUri = _baseAddress + "club/" + identifier;
            }
            else
            {
                throw new Exception("Unknown type passed into request");
            }

            return returnUri;
        }

        public async Task<T> RequestGetAsync<T>(string identifier) where T : new()
        {            
            var uri = Task.Run(() => UriGenerator<T>(HttpMethod.Get, identifier));
            
            var request = new HttpRequestMessage(HttpMethod.Get, await uri);
            var requestTask = _client.SendAsync(request);

            var httpResponse = await requestTask;
            if (httpResponse.IsSuccessStatusCode)
            {
                var resultTask = Task.Run(async () => JsonConvert.DeserializeObject<T>(await httpResponse.Content.ReadAsStringAsync()));
                return await resultTask;
            }
            else
            {
                throw new Exception($"Http request failed with error code: {httpResponse.StatusCode}");
            }
        }

        //The section below is for testing purposes only, remove after proper Service is written
        #region Testing
            
        public async Task<T> RequestGetListAsync<T>() where T : new()
        {
            if(!_isClientSetup)
                throw new Exception("Client not set up");

            string uri;

            if (typeof(T) == typeof(Models.ClubList))
            {
                uri = _baseAddress + "club/";
            }
            else if (typeof(T) == typeof(Models.MemberList))
            {
                uri = _baseAddress + "member/";
            }
            else
            {
                throw new Exception("Unknown type passed into request");
            }

            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var requestTask = _client.SendAsync(request);

            var httpResponse = await requestTask;
            if (httpResponse.IsSuccessStatusCode)
            {
                var resultTask = Task.Run(async () => JsonConvert.DeserializeObject<T>(await httpResponse.Content.ReadAsStringAsync()));
                return await resultTask;
            }
            else
            {
                throw new Exception($"Http request failed with error code: {httpResponse.StatusCode}");
            }
        }
        public async Task<User> GetUserAsync()
        {
            if (!_isClientSetup)
                throw new Exception("Client is not setup");

            var httpRequest = new HttpRequestMessage(HttpMethod.Get, _client.BaseAddress + "azuread-user/me/");

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var result = await _client.SendAsync(httpRequest);
            _logger.LogDebug($"Request took {stopwatch.Elapsed}");
            if (result.IsSuccessStatusCode)
            {
                var resultString = await result.Content.ReadAsStringAsync();
                return await Task.Run(() => JsonConvert.DeserializeObject<User>(resultString));
            }
            else
            {
                throw new Exception($"Something went wrong, http status code: {result.StatusCode}");
            }
        }
        public async Task AddClubAsync(Club club)
        {
            if (!_isClientSetup)
                throw new Exception("Client is not setup");

            
            var webClient = new WebClient();
            byte[] imageBytes = webClient.DownloadData(club.IconUrl);

            MultipartFormDataContent form = new MultipartFormDataContent
            {
                {new StringContent(club.Name), "name"},
                {new StringContent(club.Description), "description"},
                {new ByteArrayContent(imageBytes), "icon", "icon123.jpg"}
            };

            var httpRequest = new HttpRequestMessage(HttpMethod.Post, _client.BaseAddress + "club/")
            {
                Content = form
            };
            
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var result = await _client.SendAsync(httpRequest);
            _logger.LogDebug($"Request took {stopwatch.Elapsed}");
            if (!result.IsSuccessStatusCode)
                throw new Exception($"Something went wrong, http status code: {result.StatusCode}");
        }
        #endregion
    }
}
