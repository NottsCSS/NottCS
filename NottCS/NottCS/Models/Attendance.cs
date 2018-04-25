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
        Absent = 0,

        Present = 1
    }

    internal class Attendance
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "attendance")]
        public AttendanceStatus AttendanceStatus { get; set; }

        [JsonProperty(PropertyName = "feedback")]
        public string Feedback { get; set; }

        [JsonProperty(PropertyName = "participant")]
        public string Participant { get; set; }

        [JsonProperty(PropertyName = "event_time")]
        public string EventTime { get; set; }
    }
}
