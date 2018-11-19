using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace NottCS.Services.Data.Models
{
    internal class Club
    {

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        [MaxLength(500)]
        public string ImageUrl { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }
    }
}
