using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Abstract;
using Abstract.DTO;
using AutoMapper;
using Dal.Helpers;
using Dal.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace WebApp.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        private readonly AppSettings _appSettings;
        private IMapper _mapper;
        public UserController(IUserService userService, IOptions<AppSettings> appSettings, IMapper mapper)
        {
            _userService = userService;
            _appSettings = appSettings.Value;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost("auth")]
        public IActionResult Authenticate([FromBody]Auth auth)
        {
            var user = _userService.Authenticate(auth.Username, auth.Password);
            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);


            return Ok(new
            {
                user.Id,
                user.UserName,
                user.FirstName,
                user.LastName,
                tokenString
            });
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody]Register model)
        {
            var user = _mapper.Map<User>(model);

            try
            {
                _userService.Add(user, model.Password);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET: api/User
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _userService.GetUsers();
        }

        // GET: api/User/5
        [HttpGet("{id}", Name = "GetUsers")]
        public User Get(string id)
        {
            return _userService.Get(id);
        }

        [HttpGet("Name/{username}", Name = "GetByUsername")]
        public User GetByUsername(string username)
        {
            return _userService.GetByUsername(username);
        }

        [HttpGet("EventCreated/{username}")]
        public IEnumerable<Event> GetEventCreated(string username)
        {
            return _userService.GetEventCreated(username);
        }

        [HttpGet("EventJoined/{username}")]
        public IEnumerable<Event> GetEventJoined(string username)
        {
            return _userService.GetEventJoined(username);
        }

        // POST: api/User
        [HttpPost]
        public void Post([FromBody] User user, string password)
        {
            _userService.Add(user, password);
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
