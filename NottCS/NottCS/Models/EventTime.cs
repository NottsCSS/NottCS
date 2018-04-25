using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace NottCS.Models
{
    internal class EventTime
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "event")]
        public string Event { get; set; }

        [JsonProperty(PropertyName = "start_time")]
        public DateTime StartTime { get; set; }

        [JsonProperty(PropertyName = "end_time")]
        public DateTime EndTime { get; set; }
    }
}
