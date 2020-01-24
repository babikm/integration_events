using Abstract;
using Abstract.DTO;
using Dal;
using Dal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services
{
    public class UserService : ServiceBase, IUserService
    {
        public UserService(IUnitOfWork unitOfWork) : base(unitOfWork)
        { 
        }

        public void Add(User user)
        {
            _unitOfWork.UserRepository.Create(new User
            {
                Id = user.Id,
                UserName = user.UserName,
                Password = user.Password,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            });
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
