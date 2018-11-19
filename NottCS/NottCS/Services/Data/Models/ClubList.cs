using System;
using System.Collections.Generic;
using System.Text;
using NottCS.Services.Data.Club;

namespace NottCS.Services.Data.Models
{
    public class ClubList
    {
        public int Count { get; set; }
        public string Next { get; set; }
        public string Previous { get; set; }
        public List<ClubData> Results { get; set; }
    }
}
