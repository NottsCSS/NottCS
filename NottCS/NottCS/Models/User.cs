using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using SQLite;

namespace NottCS.Models
{
    public class User
    {
        [PrimaryKey]
        public int Id { get; set; }
        [MaxLength(80)]
        public string Name { get; set; }
        [MaxLength(150)]
        public string Email { get; set; }
        [MaxLength(15), JsonProperty("student_id")]
        public string StudentId { get; set; }
        [MaxLength(25), JsonProperty("library_no")]
        public string LibraryNumber { get; set; }
        [MaxLength(25), JsonProperty("year_of_study")]
        public string YearOfStudy { get; set; }
        [MaxLength(50), JsonProperty("course")]
        public string Course { get; set; }
    }
}
