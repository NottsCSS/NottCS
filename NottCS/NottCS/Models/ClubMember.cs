using System;
using Newtonsoft.Json;

namespace NottCS.Models
{
    internal enum MemberStatus
    {
        Pending = 0,
        Approved = 1,
        Canceled = 2
    }

    internal class ClubMember
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "user")]
        public string User { get; set; }

        [JsonProperty(PropertyName = "club")]
        public string Club { get; set; }

        [JsonProperty(PropertyName = "status")]
        public MemberStatus Status { get; set; }

        [JsonProperty(PropertyName = "position")]
        public string Position { get; set; }

        [JsonProperty(PropertyName = "date_created")]
        public DateTime DateCreated { get; set; }

        [JsonProperty(PropertyName = "date_modified")]
        public DateTime DateModified { get; set; }
    }
}
