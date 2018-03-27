using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace NottCS.Models
{
    internal class Participant
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "additional_file")]
        public Object AdditionalFile { get; set; }

        [JsonProperty(PropertyName = "additional_info")]
        public string AdditionalInfo { get; set; }

        [JsonProperty(PropertyName = "user")]
        public User User { get; set; }

        [JsonProperty(PropertyName = "event")]
        public Event Event { get; set; }
    }
}
