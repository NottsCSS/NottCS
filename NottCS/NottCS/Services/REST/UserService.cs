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
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, requestUri);
            var httpResponse = await Client.SendAsync(httpRequest);
            var jObject = JObject.Parse(await httpResponse.Content.ReadAsStringAsync());

            return jObject.ToObject<User>();
        }

        /// <summary>
        /// Sends a POST request to server and create a new user data
        /// </summary>
        /// <param name="user">User data to be created</param>
        /// <returns></returns>
        public async Task<bool> CreateUser(User user)
        {
            var requestUri = UriGenerator(ServiceType.CreateUser);
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, requestUri)
            {
                Content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json")
            };
            var httpResponse = await Client.SendAsync(httpRequest);

            return httpResponse.IsSuccessStatusCode;
        }

        /// <summary>
        /// Sends a POST request to server and update existing user data
        /// </summary>
        /// <param name="username">Username of the user to be modified</param>
        /// <param name="user">User data to be modified</param>
        /// <returns></returns>
        public async Task<bool> UpdateUserByUsername(string username, User user)
        {
            var requestUri = UriGenerator(ServiceType.UpdateUserByUsername, username);
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, requestUri)
            {
                Content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json")
            };
            var httpResponse = await Client.SendAsync(httpRequest);

            return httpResponse.IsSuccessStatusCode;
        }

        /// <summary>
        /// Sends a DELETE request to server and delete existing user data
        /// </summary>
        /// <param name="username">Username of the user to be deleted</param>
        /// <returns></returns>
        public async Task<bool> DeleteUserByUsername(string username)
        {
            var requestUri = UriGenerator(ServiceType.DeleteUserByUsername, username);
            var httpRequest = new HttpRequestMessage(HttpMethod.Delete, requestUri);
            var httpResponse = await Client.SendAsync(httpRequest);

            return httpResponse.IsSuccessStatusCode;
        }
    }
}
