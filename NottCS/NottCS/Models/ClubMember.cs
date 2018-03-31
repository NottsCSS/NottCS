using System;
using Newtonsoft.Json;

namespace NottCS.Models
{
    internal class ClubMember
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "user")]
        public User User { get; set; }

        [JsonProperty(PropertyName = "club")]
        public Club Club { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "position")]
        public string Position { get; set; }

        [JsonProperty(PropertyName = "date_created")]
        public DateTime DateCreated { get; set; }

        [JsonProperty(PropertyName = "date_modified")]
        public DateTime DateModified { get; set; }
    }
}
