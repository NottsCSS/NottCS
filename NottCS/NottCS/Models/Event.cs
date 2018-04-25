using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NottCS.Converters;
using Xamarin.Forms;

namespace NottCS.Models
{
    public enum EventStatus
    {
        Pending = 0,

        Started = 1,

        Ended = 3,

        Canceled = 4
    }

    public class Event
    {

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "created_timestamp")]
        public DateTime CreatedTimeStamp { get; set; }

        [JsonProperty(PropertyName = "status")]
        public EventStatus Status { get; set; }

        [JsonProperty(PropertyName = "image")]
        public string EventImage { get; set; }

        [JsonProperty(PropertyName = "venue")]
        public string Venue { get; set; }

        [JsonProperty(PropertyName = "organizing_club")]
        public string OrganizingClub { get; set; }

        [JsonProperty(PropertyName = "organizing_chairman")]
        public string OrganizingChairman { get; set; }
    }
}
