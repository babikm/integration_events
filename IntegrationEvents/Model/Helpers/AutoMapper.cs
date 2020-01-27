using Abstract.DTO;
using AutoMapper;
using Dal.Model;


namespace Dal.Helpers
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<User, Auth>();
            CreateMap<Register, User>();
        }
    }
}
