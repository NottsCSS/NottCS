using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NottCS.Models;

namespace NottCS.Services.REST
{
    internal class BaseRestService
    {
        //TODO: Update the Uri when the domain name is available
        /// <summary>
        /// Base address where the API endpoints are stored
        /// </summary>
        private const string BaseAddress = "https://testing-endpoints.herokuapp.com/";

        //TODO: Setup client with proper headers
        /// <summary>
        /// Client with setup. To add Bearer Authorization, intitialize client with SetupClient(string accessToken)
        /// </summary>
        private static readonly HttpClient Client = new HttpClient()
        {
            BaseAddress = new Uri(BaseAddress),
            Timeout = new TimeSpan(0, 0, 10)
        };

        /// <summary>
        /// Setups the Client with authorization header
        /// </summary>
        /// <param name="accessToken"></param>
        public static void SetupClient(string accessToken)
        {
            Debug.WriteLine("HttpClient is setting up...");
            Client.DefaultRequestHeaders.Clear();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }

        /// <summary>
        /// Generates the proper HttpRequestMessage with body serialized into JSON
        /// </summary>
        /// <param name="httpMethod">Request Method</param>
        /// <param name="requestUri">Request Uri</param>
        /// <param name="requestBody">Request body to be serialized</param>
        /// <returns></returns>
        private static HttpRequestMessage HttpRequestMessageGenerator(HttpMethod httpMethod, string requestUri, object requestBody = null)
        {
            #region ObjectValidator

            if (httpMethod == HttpMethod.Post && requestBody == null)
            {
                Debug.WriteLine("[BaseRestService] WARNING : No valid request body");
            }

            #endregion

            //TODO: Check if the function generates proper HttpRequestMessage
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
        private static string UriGenerator<T>(HttpMethod httpMethod, string identifier = null)
        {
            var returnUri = BaseAddress + "/azuread-user/me/";

            //TODO: Write a Uri generator logic based on the REST endpoint

            return returnUri;
        }

        /// <summary>
        /// Sends a DELETE request to the server
        /// </summary>
        /// <typeparam name="T">Type of delete object</typeparam>
        /// <param name="identifier">Identifier for server to lookup object</param>
        /// <returns>Operation status success</returns>
        public static async Task<bool> RequestDeleteAsync<T>(string identifier)
        {
            var requestUri = UriGenerator<T>(HttpMethod.Delete, identifier);
            var httpRequest = HttpRequestMessageGenerator(HttpMethod.Delete, requestUri);

            try
            {
                var requestTask = Client.SendAsync(httpRequest);
                var httpResponse = await requestTask;
                return httpResponse.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Expected exception {e.Message} at {e.TargetSite}");
                return false;
            }
        }

        /// <summary>
        /// Sends a GET request to the server 
        /// </summary>
        /// <typeparam name="T">Type of request object</typeparam>
        /// <param name="identifier">Identifier for the server to lookup</param>
        /// <returns>Requested Object</returns>
        public static async Task<Tuple<bool, T>> RequestGetAsync<T>(string identifier = null) where T : new()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var requestUri = UriGenerator<T>(HttpMethod.Get, identifier);

            Debug.WriteLine($"[GET] Generate Uri took {stopwatch.ElapsedMilliseconds} ms");
            stopwatch.Restart();

            var httpRequest = HttpRequestMessageGenerator(HttpMethod.Get, requestUri);

            Debug.WriteLine($"[GET] Generate HttpRequestMessage took {stopwatch.ElapsedMilliseconds} ms");
            stopwatch.Restart();

            try
            {
                var requestTask = Client.SendAsync(httpRequest);

                Debug.WriteLine($"[GET] Generate requestTask took {stopwatch.ElapsedMilliseconds} ms");
                stopwatch.Restart();

                //Debug.WriteLine(JsonConvert.SerializeObject(requestTask));
                var httpResponse = requestTask.GetAwaiter().GetResult();

                Debug.WriteLine($"[GET] Get response from server took {stopwatch.ElapsedMilliseconds} ms");
                stopwatch.Restart();

                if (httpResponse.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<T>(await httpResponse.Content.ReadAsStringAsync());
                    Debug.WriteLine($"{JsonConvert.SerializeObject(result)}");

                    return Tuple.Create(true, result);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }

            //TODO: Revert back to false for Item1, true is for easy login and testing purpose only
            return Tuple.Create(false, new T());
        }

        /// <summary>
        /// Sends a POST request to server to create object
        /// </summary>
        /// <typeparam name="T">Type of object to create</typeparam>
        /// <param name="objectData">Data of the object to create</param>
        /// <returns>Operation status success</returns>
        public static async Task<bool> RequestPostAsync<T>(T objectData)
        {
            var requestUri = UriGenerator<T>(HttpMethod.Post);
            var httpRequest = HttpRequestMessageGenerator(HttpMethod.Post, requestUri, objectData);

            try
            {
                var requestTask = Client.SendAsync(httpRequest);
                var httpResponse = await requestTask;
                return httpResponse.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return false;
            }
        }

        /// <summary>
        /// Sends a POST request to the server to update existing data
        /// </summary>
        /// <typeparam name="T">Type of object to update</typeparam>
        /// <param name="identifier">Identifier for th server to lookup the object</param>
        /// <param name="objectData">Data to update</param>
        /// <returns>Operation success</returns>
        public static async Task<bool> RequestUpdateAsync<T>(string identifier, T objectData)
        {
            var requestUri = UriGenerator<T>(HttpMethod.Post, identifier);
            var httpRequest = HttpRequestMessageGenerator(HttpMethod.Post, requestUri, objectData);

            try
            {
                var requestTask = Client.SendAsync(httpRequest);
                var httpResponse = await requestTask;
                return httpResponse.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return false;
            }
        }
    }
}
