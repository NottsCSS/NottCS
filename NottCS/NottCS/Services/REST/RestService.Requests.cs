using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NottCS.Services.REST
{
    internal partial class RestService
    {
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
                DebugService.WriteLine($"Expected exception {e.Message} at {e.TargetSite}");
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

            DebugService.WriteLine($"[GET] Generate Uri took {stopwatch.ElapsedMilliseconds} ms");
            stopwatch.Restart();

            var httpRequest = HttpRequestMessageGenerator(HttpMethod.Get, requestUri);

            DebugService.WriteLine($"[GET] Generate HttpRequestMessage took {stopwatch.ElapsedMilliseconds} ms");
            stopwatch.Restart();

            try
            {
                var requestTask = Client.SendAsync(httpRequest);

                DebugService.WriteLine($"[GET] Generate requestTask took {stopwatch.ElapsedMilliseconds} ms");
                stopwatch.Restart();

                //DebugService.WriteLine(JsonConvert.SerializeObject(requestTask));
                var httpResponse = requestTask.GetAwaiter().GetResult();

                DebugService.WriteLine($"[GET] Get response from server took {stopwatch.ElapsedMilliseconds} ms");
                stopwatch.Restart();

                if (httpResponse.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<T>(await httpResponse.Content.ReadAsStringAsync());
                    DebugService.WriteLine($"{JsonConvert.SerializeObject(result)}");

                    return Tuple.Create(true, result);
                }
            }
            catch (Exception e)
            {
                DebugService.WriteLine(e);
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
                DebugService.WriteLine(e);
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
                DebugService.WriteLine(e);
                return false;
            }
        }
    }
}

