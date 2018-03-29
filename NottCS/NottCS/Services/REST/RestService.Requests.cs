using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NottCS.Services.REST
{
    //TODO: Check validity of token and refresh it every time trying to connect to server
    internal static partial class RestService
    {
        /// <summary>
        /// Sends a DELETE request to the server
        /// </summary>
        /// <typeparam name="T">Type of delete object</typeparam>
        /// <param name="identifier">Identifier for server to lookup object</param>
        /// <param name="optionalClient">Optional client for other client request</param>
        /// <returns>Operation status success</returns>
        public static async Task<string> RequestDeleteAsync<T>(string identifier = null, HttpClient optionalClient = null)
        {
            var client = optionalClient ?? Client;
            var requestUri = UriGenerator<T>(HttpMethod.Delete, identifier);
            var httpRequest = HttpRequestMessageGenerator(HttpMethod.Delete, requestUri);
            string errorMessage = null;

            try
            {
                var requestTask = client.SendAsync(httpRequest);
                var httpResponse = await requestTask;
                return (httpResponse.IsSuccessStatusCode) ? "OK" : $"Something went wrong, http status code: {httpResponse.StatusCode}";
            }
            catch (Exception e)
            {
                DebugService.WriteLine(e);
                errorMessage = e.Message;
                DebugService.WriteLine($"Exception thrown in RequestPostAsync, Message: {errorMessage}");
            }
            return errorMessage;
        }

        /// <summary>
        /// Sends a GET request to the server and get a specific T type object using the identifier
        /// </summary>
        /// <typeparam name="T">Type of request object</typeparam>
        /// <param name="identifier">Identifier for the server to lookup</param>
        /// <param name="optionalClient">Optional client for other client request</param>
        /// <returns>Requested Object</returns>
        public static async Task<Tuple<string, T>> RequestGetAsync<T>(string identifier, HttpClient optionalClient = null) where T : new()
        {
            var client = optionalClient ?? Client;
            var requestUri = UriGenerator<T>(HttpMethod.Get, identifier);

            var httpRequest = HttpRequestMessageGenerator(HttpMethod.Get, requestUri);
            string errorMessage = null;

            try
            {
                var requestTask = client.SendAsync(httpRequest);
                var httpResponse = requestTask.GetAwaiter().GetResult();

                if (httpResponse.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<T>(await httpResponse.Content.ReadAsStringAsync());

                    return Tuple.Create("OK", result);
                }
            }
            catch (Exception e)
            {
                DebugService.WriteLine(e);
                errorMessage = e.Message;
                DebugService.WriteLine($"Exception thrown in RequestGetAsync, Message: {errorMessage}");
            }
            return Tuple.Create($"{errorMessage}", new T());
        }

        /// <summary>
        /// Sends a GET request to the server and gets a list of T type object
        /// </summary>
        /// <typeparam name="T">Type of request object</typeparam>
        /// <param name="optionalClient">Optional client for other client request</param>
        /// <returns></returns>
        public static async Task<Tuple<string, List<T>>> RequestGetAsync<T>(object filter = null, HttpClient optionalClient = null)
        {
            var paramFilter = filter is T variable ? variable : default(T);

            var client = optionalClient ?? Client;
            var requestUri = UriGenerator<T>(HttpMethod.Get);
            var httpRequest = HttpRequestMessageGenerator(HttpMethod.Get, requestUri);
            string errorMessage = null;
            try
            {
                var requestTask = client.SendAsync(httpRequest);
                var httpResponse = requestTask.GetAwaiter().GetResult();
                if (httpResponse.IsSuccessStatusCode)
                {
                    var resultJson = JToken.Parse(await httpResponse.Content.ReadAsStringAsync())["results"].ToString();
                    var jTokenList = JArray.Parse(resultJson).ToList();
                    List<T> resultList = new List<T>();
                    foreach (var item in jTokenList)
                    {
                        resultList.Add(item.ToObject<T>());
                    }

                    return Tuple.Create("OK", resultList);
                }
            }
            catch (Exception e)
            {
                DebugService.WriteLine(e);
                errorMessage = e.Message;
                DebugService.WriteLine($"Exception thrown in RequestGetAsync, Message: {errorMessage}");
            }

            return Tuple.Create($"{errorMessage}", new List<T>());
        }

        /// <summary>
        /// Sends a POST request to server to create object
        /// </summary>
        /// <typeparam name="T">Type of object to create</typeparam>
        /// <param name="objectData">Data of the object to create</param>
        /// <param name="optionalClient">Optional client for other client request</param>
        /// <returns>Operation status success</returns>
        public static async Task<string> RequestPostAsync<T>(T objectData, HttpClient optionalClient = null)
        {
            var client = optionalClient ?? Client;
            var requestUri = UriGenerator<T>(HttpMethod.Post);
            var httpRequest = HttpRequestMessageGenerator(HttpMethod.Post, requestUri, objectData);
            string errorMessage = null;

            try
            {
                var requestTask = client.SendAsync(httpRequest);
                var httpResponse = await requestTask;
                return (httpResponse.IsSuccessStatusCode) ? "OK" : $"Something went wrong, http status code: {httpResponse.StatusCode}";
            }
            catch (Exception e)
            {
                DebugService.WriteLine(e);
                errorMessage = e.Message;
                DebugService.WriteLine($"Exception thrown in RequestPostAsync, Message: {errorMessage}");
            }
            return errorMessage;
        }

        /// <summary>
        /// Sends a POST request to the server to update existing data
        /// </summary>
        /// <typeparam name="T">Type of object to update</typeparam>
        /// <param name="identifier">Identifier for th server to lookup the object</param>
        /// <param name="objectData">Data to update</param>
        /// <param name="optionalClient">Optional client for other client request</param>
        /// <returns>Operation success</returns>
        public static async Task<string> RequestUpdateAsync<T>(T objectData, string identifier = null, HttpClient optionalClient = null)
        {
            var client = optionalClient ?? Client;
            var requestUri = UriGenerator<T>(HttpMethod.Put, identifier);
            var httpRequest = HttpRequestMessageGenerator(HttpMethod.Put, requestUri, objectData);
            string errorMessage = null;

            try
            {
                var requestTask = client.SendAsync(httpRequest);
                var httpResponse = await requestTask;
                return (httpResponse.IsSuccessStatusCode) ? "OK" : $"Something went wrong, http status code: {httpResponse.StatusCode}";
            }
            catch (Exception e)
            {
                DebugService.WriteLine(e);
                errorMessage = e.Message;
                DebugService.WriteLine($"Exception thrown in RequestUpdateAsync, Message: {errorMessage}");
            }
            return errorMessage;
        }
    }
}

