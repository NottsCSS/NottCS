using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using NottCS.Models;

namespace NottCS.Services.REST
{
    internal class EventService : BaseRestService
    {
        /// <summary>
        /// Sends a POST request to server and create a new event data
        /// </summary>
        /// <param name="eventData">Event data to be created</param>
        /// <returns></returns>
        public static async Task<bool> CreateEvent(Event eventData)
        {
            var requestUri = UriGenerator(ServiceType.CreateEvent);
            var httpRequest = HttpRequestMessageGenerator(HttpMethod.Post, requestUri, eventData);
            var httpResponse = await Client.SendAsync(httpRequest);

            return httpResponse.IsSuccessStatusCode;
        }

        /// <summary>
        /// Sends a DELETE request to the server and delete existing event data
        /// </summary>
        /// <param name="id">ID of the event to be deleted</param>
        /// <returns></returns>
        public static async Task<bool> DeleteEventById(string id)
        {
            var requestUri = UriGenerator(ServiceType.DeleteEventById, id);
            var httpRequest = HttpRequestMessageGenerator(HttpMethod.Delete, requestUri);
            var httpResponse = await Client.SendAsync(httpRequest);

            return httpResponse.IsSuccessStatusCode;
        }

        /// <summary>
        /// Sends a GET request to the server and retrieves the event data
        /// </summary>
        /// <param name="id">ID of the event searched</param>
        /// <returns></returns>
        public static async Task<Event> GetEventById(string id)
        {
            var requestUri = UriGenerator(ServiceType.GetEventById, id);
            var httpRequest = HttpRequestMessageGenerator(HttpMethod.Get, requestUri);
            var httpResponse = await Client.SendAsync(httpRequest);
            var jObject = JObject.Parse(await httpResponse.Content.ReadAsStringAsync());

            return jObject.ToObject<Event>();
        }

        /// <summary>
        /// Sends a POST request to the server and update the existing event data
        /// </summary>
        /// <param name="id">ID of the event to be edited</param>
        /// <param name="eventData">Event data to be updated</param>
        /// <returns></returns>
        public static async Task<bool> UpdateEventById(string id, Event eventData)
        {
            var requestUri = UriGenerator(ServiceType.UpdateEventById, id);
            var httpRequest = HttpRequestMessageGenerator(HttpMethod.Post, requestUri, eventData);
            var httpResponse = await Client.SendAsync(httpRequest);

            return httpResponse.IsSuccessStatusCode;
        }
    }
}
