using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using NottCS.Models;

namespace NottCS.Services.REST
{
    internal class ClubMemberService : BaseRestService
    {
        /// <summary>
        /// Sends a POST request to server and create a new club member data
        /// </summary>
        /// <param name="member">Club member data to be created</param>
        /// <returns></returns>
        public static async Task<bool> CreateMember(ClubMember member)
        {
            var requestUri = UriGenerator(ServiceType.CreateClubMember);
            var httpRequest = HttpRequestMessageGenerator(HttpMethod.Post, requestUri, member);
            var httpResponse = await Client.SendAsync(httpRequest);

            return httpResponse.IsSuccessStatusCode;
        }

        /// <summary>
        /// Sends a DELETE request to the server and delete existing club member data
        /// </summary>
        /// <param name="id">ID of the club member to be deleted</param>
        /// <returns></returns>
        public static async Task<bool> DeleteMemberById(string id)
        {
            var requestUri = UriGenerator(ServiceType.DeleteClubMemberById, id);
            var httpRequest = HttpRequestMessageGenerator(HttpMethod.Delete, requestUri);
            var httpResponse = await Client.SendAsync(httpRequest);

            return httpResponse.IsSuccessStatusCode;
        }

        /// <summary>
        /// Sends a GET request to the server and retrieves the club member data
        /// </summary>
        /// <param name="id">ID of the club member searched</param>
        /// <returns></returns>
        public static async Task<ClubMember> GetClubMemberById(string id)
        {
            var requestUri = UriGenerator(ServiceType.GetClubMemberById, id);
            var httpRequest = HttpRequestMessageGenerator(HttpMethod.Get, requestUri);
            var httpResponse = await Client.SendAsync(httpRequest);
            var jObject = JObject.Parse(await httpResponse.Content.ReadAsStringAsync());

            return jObject.ToObject<ClubMember>();
        }

        /// <summary>
        /// Sends a POST request to the server and update the existing data with a new one
        /// </summary>
        /// <param name="id">ID of the club member to be updated</param>
        /// <param name="clubMember">Club member data to be updated</param>
        /// <returns></returns>
        public static async Task<bool> UpdateClubMemberById(string id, ClubMember clubMember)
        {
            var requestUri = UriGenerator(ServiceType.UpdateClubMemberById, id);
            var httpRequest = HttpRequestMessageGenerator(HttpMethod.Post, requestUri, clubMember);
            var httpResponse = await Client.SendAsync(httpRequest);

            return httpResponse.IsSuccessStatusCode;
        }
    }
}
