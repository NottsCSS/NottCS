using System;
using System.Collections.Generic;
using System.Text;

namespace NottCS.Models
{
    public class Event
    {
        public enum EventStatus
        {
            Pending = 0,

            Started = 1,

            Ended = 3,

            Canceled = 4
        }
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreatedTimeStamp { get; set; }

        public EventStatus Status { get; set; }

        public string EventImage { get; set; }

        public string Venue { get; set; }

        public string OrganizingClub { get; set; }

        public string OrganizingChairman { get; set; }
    }
}