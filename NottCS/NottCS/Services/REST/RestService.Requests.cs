using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NottCS.Services.REST
{
    internal static partial class RestService
    {
        /// <summary>
        /// Sends a DELETE request to the server
        /// </summary>
        /// <typeparam name="T">Type of delete object</typeparam>
        /// <param name="identifier">Identifier for server to lookup object</param>
        /// <param name="optionalClient">Optional client for other client request</param>
        /// <returns>Operation status success</returns>
        public static async Task<string> RequestDeleteAsync<T>(string identifier, HttpClient optionalClient = null)
        {
            var client = optionalClient ?? Client;
            var requestUri = UriGenerator<T>(HttpMethod.Delete, identifier);
            var httpRequest = HttpRequestMessageGenerator(HttpMethod.Delete, requestUri);

            try
            {
                var requestTask = client.SendAsync(httpRequest);
                var httpResponse = await requestTask;
                return (httpResponse.IsSuccessStatusCode) ? "OK" : "Something went wrong";
            }
            catch (Exception e)
            {
                DebugService.WriteLine($"Expected exception {e.Message} at {e.TargetSite}");
                return "Something went wrong";
            }
        }

        /// <summary>
        /// Sends a GET request to the server 
        /// </summary>
        /// <typeparam name="T">Type of request object</typeparam>
        /// <param name="identifier">Identifier for the server to lookup</param>
        /// <param name="optionalClient">Optional client for other client request</param>
        /// <returns>Requested Object</returns>
        public static async Task<Tuple<string, T>> RequestGetAsync<T>(string identifier = null, HttpClient optionalClient = null) where T : new()
        {
            var client = optionalClient ?? Client;
            var requestUri = UriGenerator<T>(HttpMethod.Get, identifier);

            var httpRequest = HttpRequestMessageGenerator(HttpMethod.Get, requestUri);

            try
            {
                var requestTask = client.SendAsync(httpRequest);
                var httpResponse = requestTask.GetAwaiter().GetResult();

                if (httpResponse.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<T>(await httpResponse.Content.ReadAsStringAsync());
                    DebugService.WriteLine($"{JsonConvert.SerializeObject(result)}");

                    return Tuple.Create("OK", result);
                }
            }
            catch (Exception e)
            {
                DebugService.WriteLine(e);
            }

            //TODO: Revert back to false for Item1, true is for easy login and testing purpose only
            return Tuple.Create("Something went wrong", new T());
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

            try
            {
                var requestTask = client.SendAsync(httpRequest);
                var httpResponse = await requestTask;
                return (httpResponse.IsSuccessStatusCode) ? "OK" : "Something went wrong";
            }
            catch (Exception e)
            {
                DebugService.WriteLine(e);
                return "Something went wrong";
            }
        }

        /// <summary>
        /// Sends a POST request to the server to update existing data
        /// </summary>
        /// <typeparam name="T">Type of object to update</typeparam>
        /// <param name="identifier">Identifier for th server to lookup the object</param>
        /// <param name="objectData">Data to update</param>
        /// <param name="optionalClient">Optional client for other client request</param>
        /// <returns>Operation success</returns>
        public static async Task<string> RequestUpdateAsync<T>(string identifier, T objectData, HttpClient optionalClient = null)
        {
            var client = optionalClient ?? Client;
            var requestUri = UriGenerator<T>(HttpMethod.Post, identifier);
            var httpRequest = HttpRequestMessageGenerator(HttpMethod.Put, requestUri, objectData);

            try
            {
                var requestTask = client.SendAsync(httpRequest);
                var httpResponse = await requestTask;
                return (httpResponse.IsSuccessStatusCode) ? "OK" : "Something went wrong";
            }
            catch (Exception e)
            {
                DebugService.WriteLine(e);
                return "Something went wrong";
            }
        }

        public static async Task<Tuple<bool, T>> RequestPutAsync<T>(T data, HttpClient client = null) where T : new()
        {
            if (client == null)
            {
                client = Client;
            }
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var requestUri = UriGenerator<T>(HttpMethod.Put);

            DebugService.WriteLine($"[PUT] Generate Uri took {stopwatch.ElapsedMilliseconds} ms");
            stopwatch.Restart();

            var httpRequest = HttpRequestMessageGenerator(HttpMethod.Put, requestUri, data);

            DebugService.WriteLine($"[PUT] Generate HttpRequestMessage took {stopwatch.ElapsedMilliseconds} ms");
            stopwatch.Restart();

            try
            {
                var requestTask = client.SendAsync(httpRequest);

                DebugService.WriteLine($"[PUT] Generate requestTask took {stopwatch.ElapsedMilliseconds} ms");
                stopwatch.Restart();

                //DebugService.WriteLine(JsonConvert.SerializeObject(requestTask));
                HttpResponseMessage httpResponse = null;
                try
                {
                    httpResponse = await requestTask;
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.GetType());
                    Debug.WriteLine(e.Message);
                }

                DebugService.WriteLine($"[PUT] Get response from server took {stopwatch.ElapsedMilliseconds} ms");
                stopwatch.Restart();
                if (httpResponse == null)
                {
                    Debug.WriteLine("Null http response?");
                    return Tuple.Create(false, new T());
                }

                var readContentTask = Task.Run(async() => await httpResponse.Content.ReadAsStringAsync());

                if (httpResponse.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<T>(await readContentTask);
                    DebugService.WriteLine($"{JsonConvert.SerializeObject(result)}");

                    return Tuple.Create(true, result);
                }
                else
                {
                    Debug.WriteLine($"[PUT]Status code: {httpResponse.StatusCode}");
                    Debug.WriteLine($"[PUT]Message: {await readContentTask}");
                }
            }
            catch (Exception e)
            {
                DebugService.WriteLine(e);
            }

            //TODO: Revert back to false for Item1, true is for easy login and testing purpose only
            return Tuple.Create(false, new T());

        }
    }
}

