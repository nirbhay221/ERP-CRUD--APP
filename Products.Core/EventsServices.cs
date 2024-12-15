using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Products.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Core
{
    public class EventsServices : IEventsServices
    {
        private readonly AppDbContext _context;
        private readonly User _user;

        public EventsServices(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _user = _context.Users.FirstOrDefault(u => u.Username == httpContextAccessor.HttpContext.User.Identity.Name);
            if (_user == null)
            {
                throw new UnauthorizedAccessException("User not found.");
            }
        }

        public Event CreateEvent(Event eventObj)
        {
            if (eventObj == null)
            {
                throw new ArgumentNullException(nameof(eventObj));
            }

            _context.Events.Add(eventObj);
            _context.SaveChanges();

            return eventObj;
        }

        public void DeleteEvent(Event eventObj)
        {
            var dbEvent = _context.Events.FirstOrDefault(e => e.Id == eventObj.Id);

            if (dbEvent == null)
            {
                throw new KeyNotFoundException("Event not found.");
            }

            _context.Events.Remove(dbEvent);
            _context.SaveChanges();
        }

        public Event EditEvent(Event eventObj)
        {
            var dbEvent = _context.Events.FirstOrDefault(e => e.Id == eventObj.Id);

            if (dbEvent == null)
            {
                throw new KeyNotFoundException("Event not found.");
            }

            dbEvent.Name = eventObj.Name;
            dbEvent.Description = eventObj.Description;
            dbEvent.Date = eventObj.Date;
            dbEvent.Location = eventObj.Location;

            _context.SaveChanges();

            return dbEvent;
        }

        public Event GetEvent(int id)
        {
            var dbEvent = _context.Events.Include(e => e.EventProjects)
                                         .Include(e => e.UserEvents)
                                         .FirstOrDefault(e => e.Id == id);

            if (dbEvent == null)
            {
                throw new KeyNotFoundException("Event not found.");
            }

            return dbEvent;
        }

        public List<Event> GetEvents()
        {
            return _context.Events.Include(e => e.EventProjects)
                                   .Include(e => e.UserEvents)
                                   .ToList();
        }
    }
}
