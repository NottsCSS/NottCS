using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Newtonsoft.Json;

namespace NottCS.Models
{
    internal class User
    {
        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; }

        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "library_number")]
        public string LibraryNumber { get; set; }

        [JsonProperty(PropertyName = "student_id")]
        public string StudentId { get; set; }

        [JsonProperty(PropertyName = "course")]
        public string Course { get; set; }

        public User()
        {
            Debug.WriteLine("Calling the default constructor for User");
            Username = "null";
            Name = "null";
            LibraryNumber = "null";
            StudentId = "null";
            Course = "null";
        }
    }
}
