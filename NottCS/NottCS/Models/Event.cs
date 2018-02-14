using System;
using System.Collections.Generic;
using System.Text;

namespace NottCS.Models
{
    internal class Event
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public DateTime Timestamp { get; set; }
        public string OrganizingClubId { get; set; }
        public string OrganizingChairperson { get; set; }
        public string Status { get; set; }
        public Uri ImageUri { get; set; }
    }
}
