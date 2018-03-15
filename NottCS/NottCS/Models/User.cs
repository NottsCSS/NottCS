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

        [JsonProperty(PropertyName = "course")]
        public string Course { get; set; }

        [JsonProperty(PropertyName = "year_of_study")]
        public int YearOfStudy { get; set; }

        public User()
        {
            Name = "Eagle Cheow";
            Email = "noemail@nottingham.edu.my";
            StudentId = "18818888";
            LibraryNumber = "2001438888";
            Course = "Software Engineering";
            YearOfStudy = 2;
            IsAuthenticated = false;
        }
    }
}
