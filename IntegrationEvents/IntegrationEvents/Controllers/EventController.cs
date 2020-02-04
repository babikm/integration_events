using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abstract;
using Abstract.DTO;
using Dal.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace WebApp.Controllers
{
    [Authorize]
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
        public IActionResult Join(string userId, string eventId)
        {
            if(_eventService.Join(userId, eventId))
                return Ok("Dołączyłeś do wydarzenia!");            
            return BadRequest("Już wcześniej dołączyłeś do tego wydarzenia!");
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
        public void Put(Event @event, string id)
        {
            _eventService.Update(@event, id);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _eventService.Delete(id);
        }
    }
}
