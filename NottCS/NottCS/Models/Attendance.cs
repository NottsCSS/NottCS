using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace NottCS.Models
{
    internal class AttendanceStatus
    {
        public static readonly string Absent = "ABSENT";
        public static readonly string Present = "PRESENT";
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
        public Participant Participant { get; set; }

        [JsonProperty(PropertyName = "event_time")]
        public EventTime EventTime { get; set; }
    }
}
