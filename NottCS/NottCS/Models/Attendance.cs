using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace NottCS.Models
{
    internal enum AttendanceStatus
    {
        [EnumMember(Value = "ABSENT")]
        Absent,

        [EnumMember(Value = "PRESENT")]
        Present
    }

    internal class Attendance
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "attendance")]
        [JsonConverter(typeof(StringEnumConverter))]
        public AttendanceStatus AttendanceStatus { get; set; }

        [JsonProperty(PropertyName = "feedback")]
        public string Feedback { get; set; }

        [JsonProperty(PropertyName = "participant")]
        public Participant Participant { get; set; }

        [JsonProperty(PropertyName = "event_time")]
        public EventTime EventTime { get; set; }
    }
}
