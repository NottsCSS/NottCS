using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Xamarin.Forms;

namespace NottCS.Models
{
    internal class Club
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "icon")]
        public string Image { get; set; }

        [JsonProperty(PropertyName = "created_timestamp")]
        public DateTime CreatedTimeStamp { get; set; }

        [JsonProperty(PropertyName = "updated_timestamp")]
        public DateTime UpdatedTimeStamp { get; set; }
    }
}
