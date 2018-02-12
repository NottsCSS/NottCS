using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NottCS.Services.REST
{
    internal class BaseRestService
    {
        //TODO: Update the Uri when the domain name is available
        private const string BaseAddress = "https://apis.nottcs.com/";

        //TODO: Setup client with proper headers
        protected static readonly HttpClient Client = new HttpClient()
        {
            Timeout = new TimeSpan(0, 0, 5)
        };

        protected const int TimeoutPeriod = 1000;

        /// <summary>
        /// Available service
        /// </summary>
        protected enum ServiceType
        {
            GetUserByUsername, CreateUser, UpdateUserByUsername, DeleteUserByUsername,
            CreateClub, DeleteClubById, GetClubById, UpdateClubById,
            CreateClubMember, DeleteClubMemberById, GetClubMemberById, UpdateClubMemberById,
            CreateEvent, DeleteEventById, GetEventById, UpdateEventById,
        }

        /// <summary>
        /// Generates the proper HttpRequestMessage with body serialized into JSON
        /// </summary>
        /// <param name="httpMethod">Request Method</param>
        /// <param name="requestUri">Request Uri</param>
        /// <param name="requestBody">Request body to be serialized</param>
        /// <returns></returns>
        protected static HttpRequestMessage HttpRequestMessageGenerator(HttpMethod httpMethod, string requestUri, object requestBody = null)
        {
            #region ObjectValidator

            if (httpMethod == HttpMethod.Post && requestBody == null)
            {
                Debug.WriteLine("[BaseRestService] WARNING : No valid request body");
            }

            #endregion

            //TODO: Check if the function generates proper HttpRequestMessage
            var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
            var httpRequest = new HttpRequestMessage(httpMethod, requestUri)
            {
                Content = content
            };
            return httpRequest;
        }

        /// <summary>
        /// Generates appropriate type of REST Uri for requests
        /// </summary>
        /// <param name="service">Request Type</param>
        /// <param name="identifier">Identifier for search query, usually the primary key</param>
        /// <returns></returns>
        protected static string UriGenerator(ServiceType service, string identifier = null)
        {
            string returnUri;

            #region IdentifierValidator

            //Gives warning if identifier is invalid
            var createRequestList = new List<ServiceType>()
            {
                ServiceType.CreateClub, ServiceType.CreateClubMember, ServiceType.CreateEvent, ServiceType.CreateUser
            };

            if (!createRequestList.Contains(service) && identifier == null)
            {
                Debug.WriteLine("[BaseRestService] WARNING : No valid identifier for UriGenerator");
            }

            #endregion

            //TODO: Update the Uri when the documentation from the backend is ready
            switch (service)
            {
                case ServiceType.CreateUser:
                {
                    returnUri = BaseAddress + "/user/createUser";
                    break;
                }
                case ServiceType.DeleteUserByUsername:
                {
                    returnUri = BaseAddress + "/user/deleteUserByUsername/" + identifier;
                    break;
                }
                case ServiceType.GetUserByUsername:
                {
                    returnUri = BaseAddress + "/user/getUserByUsername/" + identifier;
                    break;
                }
                case ServiceType.UpdateUserByUsername:
                {
                    returnUri = BaseAddress + "/user/updateUserByUsername/" + identifier;
                    break;
                }
                case ServiceType.CreateClub:
                {
                    returnUri = BaseAddress + "/club/createClub";
                    break;
                }
                case ServiceType.DeleteClubById:
                {
                    returnUri = BaseAddress + "/club/deleteClubById/" + identifier;
                    break;
                }
                case ServiceType.GetClubById:
                {
                    returnUri = BaseAddress + "/club/getClubById/" + identifier;
                    break;
                }
                case ServiceType.UpdateClubById:
                {
                    returnUri = BaseAddress + "/club/updateClubById" + identifier;
                    break;
                }
                case ServiceType.CreateClubMember:
                {
                    returnUri = BaseAddress + "/clubMember/createClubMember";
                    break;
                }
                case ServiceType.DeleteClubMemberById:
                {
                    returnUri = BaseAddress + "/clubMember/deleteClubMemberById/" + identifier;
                    break;
                }
                case ServiceType.GetClubMemberById:
                {
                    returnUri = BaseAddress + "/clubMember/getClubMemberById/" + identifier;
                    break;
                }
                case ServiceType.UpdateClubMemberById:
                {
                    returnUri = BaseAddress + "/clubMember/updateClubMemberById/" + identifier;
                    break;
                }
                case ServiceType.CreateEvent:
                {
                    returnUri = BaseAddress + "/event/createEvent";
                    break;
                }
                case ServiceType.DeleteEventById:
                {
                    returnUri = BaseAddress + "/event/deleteEventById/" + identifier;
                    break;
                }
                case ServiceType.GetEventById:
                {
                    returnUri = BaseAddress + "/event/getEventById/" + identifier;
                    break;
                }
                case ServiceType.UpdateEventById:
                {
                    returnUri = BaseAddress + "/event/updateEventById/" + identifier;
                    break;
                }
                default:
                {
                    returnUri = BaseAddress + "/apiNotFound";
                    break;
                }
            }

            return returnUri;
        }

        private static string UriGenerator<T>(HttpMethod httpMethod, string identifier = null)
        {
            var returnUri = "";

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
                Debug.WriteLine(e);
                return false;
            }
        }

        /// <summary>
        /// Sends a GET request to the server 
        /// </summary>
        /// <typeparam name="T">Type of request object</typeparam>
        /// <param name="identifier">Identifier for the server to lookup</param>
        /// <returns>Requested Object</returns>
        public static async Task<Tuple<bool, T>> RequestGetAsync<T>(string identifier) where T : new()
        {
            var requestUri = UriGenerator<T>(HttpMethod.Get, identifier);
            var httpRequest = HttpRequestMessageGenerator(HttpMethod.Get, requestUri);
            try
            {
                var requestTask = Client.SendAsync(httpRequest);
                var httpResponse = await requestTask;
                if (httpResponse.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<T>(await httpResponse.Content.ReadAsStringAsync());
                    return Tuple.Create(true, result);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }

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
