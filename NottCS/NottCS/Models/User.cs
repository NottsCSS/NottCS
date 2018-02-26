using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Newtonsoft.Json;

namespace NottCS.Models
{
    public class User
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "student_id")]
        public string StudentId { get; set; }

        [JsonProperty(PropertyName = "library_no")]
        public string LibraryNumber { get; set; }

        [JsonProperty(PropertyName = "is_authenticated")]
        public bool IsAuthenticated { get; set; }

        [JsonProperty(PropertyName = "date_joined")]
        public DateTime DateJoined { get; set; }

        public User()
        {
            Name = "Eagle Cheow";
            Email = "noemail@nottingham.edu.my";
            StudentId = "18818888";
            LibraryNumber = "2001438888";
            IsAuthenticated = false;
            DateJoined = DateTime.Now;
        }
    }
}
