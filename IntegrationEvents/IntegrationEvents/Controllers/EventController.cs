using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abstract;
using Abstract.DTO;
using Dal.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private IEventService _eventService;
        public EventController(IEventService eventService)
        {
            _eventService = eventService;
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
        [HttpPost]
        public void Post([FromBody] Event @event)
        {
            _eventService.Add(@event);
        }

        // PUT: api/Event/5
        [HttpPut("{id}")]
        public void Put([FromBody] Event @event)
        {
            _eventService.Update(@event);
        }

        // DELETE: api/ApiWithActions/5
        [HttpPost("Delete/{id}")]
        public void Delete(string id)
        {
            _eventService.Delete(id);
        }
    }
}
