using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NottCS.Helpers;
using NottCS.Models;
using NottCS.ViewModels;

namespace NottCS.Services.REST
{
    //TODO: Write unit test for REST
    internal static partial class RestService
    {


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
            DebugService.WriteLine("HttpClient is setting up...");
            Client.DefaultRequestHeaders.Clear();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }

        /// <summary>
        /// Resets the authorization header
        /// </summary>
        public static void ResetClient()
        {
            Client.DefaultRequestHeaders.Clear();
        }

        private static async Task<HttpRequestMessage> HttpRequestMessageGenerator(HttpMethod httpMethod, string requestUri, object requestBody = null)
        {
            var isValidToken = false;
            if (!GlobalUserData.isValidToken)
            {
                isValidToken = await LoginService.SignInMicrosoftAsync();
            }
            if (!isValidToken)
            {
                Navigation.NavigationService.ClearNavigation();
                await Navigation.NavigationService.NavigateToAsync<LoginViewModel>();
            }
            #region ObjectValidator

            if (httpMethod == HttpMethod.Post && requestBody == null)
            {
                DebugService.WriteLine("[BaseRestService] WARNING : No valid request body");
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
        private static string UriGenerator<T>(HttpMethod httpMethod, string identifier = null)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            string returnUri;
            DebugService.WriteLine($"[{stopwatch.ElapsedMilliseconds}] UriGenerator called");

            if (typeof(T) == typeof(User))
            {
                returnUri = BaseAddress + "azuread-user/me/";
            }
            else if (typeof(T) == typeof(Event))
            {
                returnUri = BaseAddress + "event/" + identifier;
            }
            else if (typeof(T) == typeof(EventTime))
            {
                returnUri = BaseAddress + "event-time/" + identifier;
            }
            else if (typeof(T) == typeof(Participant))
            {
                returnUri = BaseAddress + "participant/" + identifier;
            }
            else if (typeof(T) == typeof(Club))
            {
                returnUri = BaseAddress + "club/" + identifier;
            }
            else if (typeof(T) == typeof(ClubMember))
            {
                returnUri = BaseAddress + "member/" + identifier;
            }
            else if (typeof(T) == typeof(Attendance))
            {
                returnUri = BaseAddress + "attendence/" + identifier;
            }
            else
            {
                returnUri = BaseAddress + "unknown/";
            }
            DebugService.WriteLine($"[{stopwatch.ElapsedMilliseconds}] The request URI is {returnUri}");
            return returnUri;
        }

    }
}
