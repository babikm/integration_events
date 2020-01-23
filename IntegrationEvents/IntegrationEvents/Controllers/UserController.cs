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
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        // GET: api/User
        [HttpGet]
        public IEnumerable<UserDto> Get()
        {
            return _userService.GetUsers();
        }

        // GET: api/User/5
        [HttpGet("{id}", Name = "GetUsers")]
        public User Get(string id)
        {
            return _userService.Get(id);
        }

        // POST: api/User
        [HttpPost]
        public void Post([FromBody] User user)
        {
            _userService.Add(user);
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public void Put([FromBody] User user)
        {
            _userService.Update(user, user.Id);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _userService.Delete(id);
        }
    }
}
