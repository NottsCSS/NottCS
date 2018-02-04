using System;
using System.Collections.Generic;
using System.Text;

namespace NottCS.Models
{
    internal class ClubMember
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string ClubId { get; set; }
        public string Status { get; set; }
        public string Position { get; set; }
    }
}
