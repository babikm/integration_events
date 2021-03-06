﻿using Abstract;
using Abstract.DTO;
using Dal;
using Dal.Model;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class EventService : ServiceBase, IEventService
    {
        public EventService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public void Add(Event @event)
        {
            _unitOfWork.EventRepository.Create(new Event
            {
                Id = @event.Id,
                Name = @event.Name,
                Description = @event.Description,
                Spot = @event.Spot,
                Date = @event.Date,
                UserList = new List<User>()
            });
        }

        public void Delete(string id)
        {
            var @event = _unitOfWork.EventRepository.Get(id);
            foreach (var item in @event.UserList)
            {
                var user = _unitOfWork.UserRepository.Get(item.Id);
                user.EventJoined.RemoveAll(x => x == id);
                user.EventCreated.RemoveAll(x => x == id);
                var update = Builders<User>
                    .Update
                        .Set(x => x.EventCreated, user.EventCreated)
                        .Set(x => x.EventJoined, user.EventJoined);
                    
                _unitOfWork.UserRepository.Update(x => x.Id == user.Id, update);
            }
            _unitOfWork.EventRepository.Delete(id);
        }

        public bool Join(string userId, string eventId)
        {
            //User user = new User();
            var user = _unitOfWork.UserRepository.Get(userId);
            var @event = _unitOfWork.EventRepository.Get(eventId);
            bool isExist = false;

            foreach (var item in user.EventJoined)
            {
                if (item == eventId)
                    isExist = true;
            }

            if(!isExist)
            {
                @event.UserList.Add(user);
                user.EventJoined.Add(eventId);                
                _unitOfWork.UserRepository.Update(x => x.Id == user.Id, user);
                _unitOfWork.EventRepository.Update(x =>x.Id == @event.Id, @event);
                return true;
            }

            return false;
        }

        public bool RemoveJoined(string userId, string eventId)
        {
            var user = _unitOfWork.UserRepository.Get(userId);
            var @event = _unitOfWork.EventRepository.Get(eventId);
            foreach (var item in user.EventJoined)
            {
                if (item == eventId)
                {
                    var list = @event.UserList.Where(x => x.Id != userId).ToList();                 
                    user.EventJoined.Remove(eventId);
                    var update = Builders<Event>
                            .Update
                                .Set(x => x.UserList, list);
                    _unitOfWork.EventRepository.Update(x => x.Id == @event.Id, update);
                    _unitOfWork.UserRepository.Update(x => x.Id == user.Id, user);
                    return true;

                }
            }
            return false;
        }

        public Event Get(string id)
        {
            return _unitOfWork.EventRepository.Get(id);
        }

        public IEnumerable<EventDto> GetEvents()
        {
            return _unitOfWork.EventRepository.GetAll().Select(x => new EventDto
            {
               Id = x.Id,
               Name = x.Name,
               Date = x.Date,
               Description = x.Description,
               UserList = x.UserList,
               Spot = x.Spot
            }).ToList();
        }

        public IEnumerable<Event> GetUserEvents(string userId)
        {
            var user = _unitOfWork.UserRepository.Get(userId);
            var list = user.EventJoined.AsEnumerable();
            List<Event> eventList = new List<Event>();
            foreach (var item in list)
            {
                eventList.Add(_unitOfWork.EventRepository.Get(item));
            }

            return eventList;
        }

        public void Update(Event @event, string id)
        {
            var ev = _unitOfWork.EventRepository.Get(id);
            var userList = new List<User>();

            if (@event.UserList != null)
                userList = @event.UserList;
            else
            {
                userList = ev.UserList;
            }

            var update = Builders<Event>
                .Update
                    .Set(x => x.Name, @event.Name)
                    .Set(x => x.Description, @event.Description)
                    .Set(x => x.Date, @event.Date)
                    .Set(x => x.Spot, @event.Spot)
                    .Set(x => x.UserList, userList);
            _unitOfWork.EventRepository.Update(x => x.Id == id, @update);
        }

        
    }
}
