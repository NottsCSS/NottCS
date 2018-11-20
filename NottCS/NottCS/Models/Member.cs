using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Newtonsoft.Json;
using SQLite;

namespace NottCS.Models
{
    public enum MemberStatus
    {
        Pending = 0,
        Approved = 1,
        Cancelled = 2
    }

    public class Member
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [JsonProperty("user")]
        public int UserId { get; set; }
        [JsonProperty("club")]
        public int ClubId { get; set; }
        [JsonProperty("status")]
        public MemberStatus Status { get; set; }
        [MaxLength(100)]
        public string Position { get; set; }
        [JsonProperty("date_created"),]
        public DateTime CreatedTime { get; set; }
        [JsonProperty("date_modified")]
        public DateTime UpdatedTime { get; set; }
    }
}
