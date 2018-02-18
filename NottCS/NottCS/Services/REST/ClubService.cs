using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using NottCS.Models;

namespace NottCS.Services.REST
{
    internal class ClubService : BaseRestService
    {
        /// <summary>
        /// Sends a POST request to server and create a new club data
        /// </summary>
        /// <param name="club">Club data to be created</param>
        /// <returns></returns>
        public static async Task<bool> CreateClub(Club club)
        {
            var requestUri = UriGenerator(ServiceType.CreateClub);
            var httpRequest = HttpRequestMessageGenerator(HttpMethod.Post, requestUri, club);
            var httpResponse = await Client.SendAsync(httpRequest);

            return httpResponse.IsSuccessStatusCode;
        }

        /// <summary>
        /// Sends a DELETE request to the server and delete existing club data
        /// </summary>
        /// <param name="id">ID of the club to be deleted</param>
        /// <returns></returns>
        public static async Task<bool> DeleteClubById(string id)
        {
            var requestUri = UriGenerator(ServiceType.DeleteClubById, id);
            var httpRequest = HttpRequestMessageGenerator(HttpMethod.Delete, requestUri);
            var httpResponse = await Client.SendAsync(httpRequest);

            return httpResponse.IsSuccessStatusCode;
        }

        /// <summary>
        /// Sends a GET request to the server and retrieves the club data
        /// </summary>
        /// <param name="id">ID of the club searched</param>
        /// <returns></returns>
        public static async Task<Club> GetClubById(string id)
        {
            var requestUri = UriGenerator(ServiceType.GetClubById, id);
            var httpRequest = HttpRequestMessageGenerator(HttpMethod.Get, requestUri);
            var httpResponse = await Client.SendAsync(httpRequest);
            var jObject = JObject.Parse(await httpResponse.Content.ReadAsStringAsync());

            return jObject.ToObject<Club>();
        }

        /// <summary>
        /// Sends a POST request to the server and update the existing data with a new one
        /// </summary>
        /// <param name="id">ID of the club to be updated</param>
        /// <param name="club">Club data to be updated</param>
        /// <returns></returns>
        public static async Task<bool> UpdateClubById(string id, Club club)
        {
            var requestUri = UriGenerator(ServiceType.UpdateClubById, id);
            var httpRequest = HttpRequestMessageGenerator(HttpMethod.Post, requestUri, club);
            var httpResponse = await Client.SendAsync(httpRequest);

            return httpResponse.IsSuccessStatusCode;
        }
    }
}
