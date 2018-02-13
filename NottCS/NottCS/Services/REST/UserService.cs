using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NottCS.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NottCS.Services.REST
{
    internal class UserService : BaseRestService
    {
        /// <summary>
        /// Sends a GET request to server and retrieve user data
        /// </summary>
        /// <param name="username">Username to search</param>
        /// <returns></returns>
        public static async Task<User> GetUserByUsername(string username)
        {
            var requestUri = UriGenerator(ServiceType.GetUserByUsername, username);
            var httpRequest = HttpRequestMessageGenerator(HttpMethod.Get, requestUri);
            var requestTask = Client.SendAsync(httpRequest);
            try
            {
                var response = await requestTask;
                var user = JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync());
                return user;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                //TODO: Return error
                return new User();
            }
        }

        /// <summary>
        /// Sends a POST request to server and create a new user data
        /// </summary>
        /// <param name="user">User data to be created</param>
        /// <returns></returns>
        public static async Task<bool> CreateUser(User user)
        {
            var requestUri = UriGenerator(ServiceType.CreateUser);
            var httpRequest = HttpRequestMessageGenerator(HttpMethod.Post, requestUri, user);
            var httpResponse = await Client.SendAsync(httpRequest);

            return httpResponse.IsSuccessStatusCode;
        }

        /// <summary>
        /// Sends a POST request to server and update existing user data
        /// </summary>
        /// <param name="username">Username of the user to be modified</param>
        /// <param name="user">User data to be modified</param>
        /// <returns></returns>
        public static async Task<bool> UpdateUserByUsername(string username, User user)
        {
            var requestUri = UriGenerator(ServiceType.UpdateUserByUsername, username);
            var httpRequest = HttpRequestMessageGenerator(HttpMethod.Post, requestUri, user);
            var httpResponse = await Client.SendAsync(httpRequest);

            return httpResponse.IsSuccessStatusCode;
        }

        /// <summary>
        /// Sends a DELETE request to server and delete existing user data
        /// </summary>
        /// <param name="username">Username of the user to be deleted</param>
        /// <returns></returns>
        public static async Task<bool> DeleteUserByUsername(string username)
        {
            var requestUri = UriGenerator(ServiceType.DeleteUserByUsername, username);
            var httpRequest = HttpRequestMessageGenerator(HttpMethod.Delete, requestUri);
            var httpResponse = await Client.SendAsync(httpRequest);

            return httpResponse.IsSuccessStatusCode;
        }
    }
}
