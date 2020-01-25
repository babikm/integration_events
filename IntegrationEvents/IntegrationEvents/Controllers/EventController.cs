using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abstract;
using Abstract.DTO;
using Dal.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private IEventService _eventService;
        private IUserService _userService;

        public EventController(IEventService eventService, IUserService userService)
        {
            _eventService = eventService;
            _userService = userService;
        }

        [HttpPost("Join/{eventId}/{userId}")]
        public void Join(string userId, string eventId)
        {
            User user = new User();
            user = _userService.Get(userId);
            var @event = _eventService.Get(eventId);
            @event.UserList.Add(user);
            Put(@event);
            user.EventJoined.Add(eventId);
            _userService.Update(user, user.Id);

        }
        // GET: api/Event
        [HttpGet]
        public IEnumerable<EventDto> Get()
        {
            return _eventService.GetEvents();
        }

        // GET: api/Event/5
        [HttpGet("{id}", Name = "Get")]
        public Event Get(string id)
        {
            return _eventService.Get(id);
        }

        // POST: api/Event
        [HttpPost("{userId}")]
        public void Post([FromBody] Event @event, string userId)
        {
            @event.Id = ObjectId.GenerateNewId().ToString();
            _eventService.Add(@event);
            var user = _userService.Get(userId);
            user.EventCreated.Add(@event.Id);
            _userService.Update(user, user.Id);
        }

        // PUT: api/Event/5
        [HttpPut("{id}")]
        public void Put([FromBody] Event @event)
        {
            _eventService.Update(@event, @event.Id);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _eventService.Delete(id);
        }
    }
}
