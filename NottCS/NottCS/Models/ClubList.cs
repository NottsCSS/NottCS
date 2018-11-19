using System;
using System.Collections.Generic;
using System.Text;
using NottCS.Services.Data.Club;

namespace NottCS.Models
{
    public class ClubList
    {
        public int Count { get; set; }
        public string Next { get; set; }
        public string Previous { get; set; }
        public List<Club> Results { get; set; }
    }
}
