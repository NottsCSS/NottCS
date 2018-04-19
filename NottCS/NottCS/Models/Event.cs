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
    internal enum EventStatus
    {
        [EnumMember(Value = "PD")]
        Pending,

        [EnumMember(Value = "ST")]
        Started,

        [EnumMember(Value = "ED")]
        Ended,

        [EnumMember(Value = "CC")]
        Canceled
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
        [JsonConverter(typeof(StringEnumConverter))]
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
