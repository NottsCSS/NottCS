using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NottCS.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NottCS.Services.REST
{
    class UserService
    {
        const string baseAddress = "https://api.nottcs.com/";

        static HttpClient client = new HttpClient();

        public static async Task<User> GetUserByUsername(string username)
        {
            string requestUri = baseAddress + "/getUserByUsername/" + username;
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Get, requestUri);
            HttpResponseMessage httpResponse = await client.SendAsync(httpRequest);
            JObject jObject = JObject.Parse(await httpResponse.Content.ReadAsStringAsync());

            return jObject.ToObject<User>();
        }

        public async Task<bool> CreateUser(User user)
        {
            string requestUri = baseAddress + "/createUser";
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, requestUri)
            {
                Content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json")
            };
            HttpResponseMessage httpResponse = await client.SendAsync(httpRequest);

            return httpResponse.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateUserByUsername(string username, User user)
        {
            string requestUri = baseAddress + "/updateUserByUsername/" + username;
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, requestUri)
            {
                Content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json")
            };
            HttpResponseMessage httpResponse = await client.SendAsync(httpRequest);

            return httpResponse.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteUserByUsername(string username)
        {
            string requestUri = baseAddress + "/deleteUserByUsername/" + username;
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Delete, requestUri);
            HttpResponseMessage httpResponse = await client.SendAsync(httpRequest);

            return httpResponse.IsSuccessStatusCode;
        }
    }
}
