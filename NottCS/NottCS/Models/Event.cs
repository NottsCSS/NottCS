using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace NottCS.Models
{
    public sealed class EventStatus
    {
        public static readonly string Pending = "Pending";
        public static readonly string Started = "Started";
        public static readonly string Ended = "Ended";
        public static readonly string Canceled = "Canceled";
    }

    internal class Event
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
        public Club OrganizingClub { get; set; }

        [JsonProperty(PropertyName = "organizing_chairman")]
        public User OrganizingChairman { get; set; }
    }
}
