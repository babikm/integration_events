﻿using System;
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
        private IUserService _userService;

        public EventController(IEventService eventService, IUserService userService)
        {
            _eventService = eventService;
            _userService = userService;
        }

        [HttpPost("Join")]
        public void Join(User user, string eventId)
        {
            user = _userService.Get("5e29cb13e0ede23cf0ca2000");
            var @event = _eventService.Get("5e29cb43e0ede23cf0ca2001");
            @event.UserList = new List<User>();
            @event.UserList.Add(user);
            Put(@event);
            //_eventService.Join(user, eventId);
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
