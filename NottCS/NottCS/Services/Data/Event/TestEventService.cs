using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NottCS.Models;

namespace NottCS.Services.Data.Event
{
    internal class TestEventService : IEventService
    {
        private readonly List<Models.Event> _eventList = new List<Models.Event>()
        {
            new Models.Event()
            {
                CreatedTimeStamp = DateTime.Parse("2018-05-03"),
                Description = "Just a cancelled event",
                EventImage = "https://cdn2.iconfinder.com/data/icons/circle-icons-1/64/calendar-512.png",
                Id = 1,
                OrganizingChairman = 1,
                OrganizingClub = 1,
                ParamList = new List<Forms>()
                {
                    new Forms()
                    {
                        FieldsTitle = "Phone Number"
                    },
                    new Forms()
                    {
                        FieldsTitle = "Dietary restrictions"
                    }
                },
                Status = Models.Event.EventStatus.Canceled,
                Title = "Event 1",
                Venue = "F3B09"
            },
            new Models.Event()
            {
                CreatedTimeStamp = DateTime.Parse("2018-07-07"),
                Description = "Just a event pending for confirmation",
                EventImage = "https://img.freepik.com/free-icon/election-event-on-a-calendar-with-star-symbol_318-64485.jpg?size=338&ext=jpg",
                Id = 2,
                OrganizingChairman = 2,
                OrganizingClub = 2,
                ParamList = new List<Forms>()
                {
                    new Forms()
                    {
                        FieldsTitle = "Phone Number"
                    },
                    new Forms()
                    {
                        FieldsTitle = "Dietary restrictions"
                    }
                },
                Status = Models.Event.EventStatus.Pending,
                Title = "Event 2",
                Venue = "F1A03"
            },
            new Models.Event()
            {
                CreatedTimeStamp = DateTime.Parse("2018-11-07"),
                Description = "Just an event that has started",
                EventImage = "https://static.thenounproject.com/png/41125-200.png",
                Id = 3,
                OrganizingChairman = 3,
                OrganizingClub = 3,
                ParamList = new List<Forms>()
                {
                    new Forms()
                    {
                        FieldsTitle = "Hometown"
                    },
                },
                Status = Models.Event.EventStatus.Started,
                Title = "Event 3",
                Venue = "F3C04"
            }
        };
        public Task<List<Models.Event>> GetAllEventsAsync()
        {
            return Task.FromResult(_eventList);
        }

        public Task<List<Models.Event>> GetEventByClubAsync(int clubid)
        {
            return Task.FromResult(_eventList.Where(s => s.OrganizingClub == clubid).ToList());
        }

        public Task<List<Models.Event>> GetEventByClubAsync(Models.Club club)
        {
            return GetEventByClubAsync(club.Id);
        }

        public Task<Models.Event> GetEventByIdAsync(int id)
        {
            return Task.FromResult(_eventList.FirstOrDefault(s => s.Id == id));
        }

        public Task<Models.Event> GetEventByTitleAsync(string title)
        {
            return Task.FromResult(_eventList.FirstOrDefault(s => s.Title.ToUpperInvariant() == title.ToUpperInvariant()));
        }

        public Task AddEventAsync(Models.Event Event)
        {
            _eventList.Add(Event);

            return Task.CompletedTask;
        }

        public Task DeleteEventAsync(Models.Event Event)
        {
            _eventList.Remove(Event);

            return Task.CompletedTask;
        }

        public Task DeleteEventAsync(int id)
        {
            var eventToBeDeleted = _eventList.FirstOrDefault(s => s.Id == id);
            _eventList.Remove(eventToBeDeleted);

            return Task.CompletedTask;
        }
    }
}
