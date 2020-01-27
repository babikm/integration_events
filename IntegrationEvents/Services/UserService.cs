using Abstract;
using Abstract.DTO;
using Dal;
using Dal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Services
{
    public class UserService : ServiceBase, IUserService
    {
        public UserService(IUnitOfWork unitOfWork) : base(unitOfWork)
        { 
        }

        public void Add(User user, string password)
        {
            if(password == null)
                throw new Exception("Password is required!");

            if (_unitOfWork.UserRepository.GetAll().Any(x => x.UserName == user.UserName))
                throw new Exception("Username \"" + user.UserName + "\" is already taken");

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            _unitOfWork.UserRepository.Create(new User
            {
                Id = user.Id,
                UserName = user.UserName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                EventCreated = new List<string>(),
                EventJoined = new List<string>()
            });
        }

        public User Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;
            var user = _unitOfWork.UserRepository.GetByUsername(username);

            if (user == null)
                return null;

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }


        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) 
                throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) 
                throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) 
                throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) 
                throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }

        public void Delete(string id)
        {
            _unitOfWork.UserRepository.Delete(id);
        }

        public User Get(string id)
        {
            return _unitOfWork.UserRepository.Get(id);
        }

        public User GetByUsername(string username)
        {
            var user = _unitOfWork.UserRepository.GetByUsername(username);
            return user;
        }

        public IEnumerable<Event> GetEventCreated(string username)
        {
            var user = GetByUsername(username);
            List<string> list = user.EventCreated;
            List<Event> updated = new List<Event>();
            foreach (var item in list)
            {
                updated.Add(_unitOfWork.EventRepository.Get(item));
            }
            return updated;
        }

        public IEnumerable<Event> GetEventJoined(string username)
        {
            var user = GetByUsername(username);
            List<string> list = user.EventJoined;
            List<Event> updated = new List<Event>();
            foreach (var item in list)
            {
                updated.Add(_unitOfWork.EventRepository.Get(item));
            }
            return updated;
        }

        public IEnumerable<UserDto> GetUsers()
        {
            return _unitOfWork.UserRepository.GetAll().Select(x => new UserDto
            {
                Id = x.Id,
                Username = x.UserName,
                EventCreated = x.EventCreated
            }).ToList();
        }

        public void Update(User user, string id)
        {
            _unitOfWork.UserRepository.Update(x => x.Id == id, user);            
        }
    }
}
