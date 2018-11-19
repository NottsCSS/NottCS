using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using SQLite;

namespace NottCS.Models
{
    public class Club
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        [MaxLength(500), JsonProperty("icon")]
        public string IconUrl { get; set; }
        [JsonProperty("created_timestamp"),]
        public DateTime CreatedTime { get; set; }
        [JsonProperty("updated_timestamp")]
        public DateTime UpdatedTime { get; set; }
    }
}