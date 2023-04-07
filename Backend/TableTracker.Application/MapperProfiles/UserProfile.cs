using AutoMapper;

using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Entities;

namespace TableTracker.Application.MapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>();

            CreateMap<UserDTO, User>();

            CreateMap<VisitorDTO, UserDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Avatar, opt => opt.MapFrom(srs => srs.Avatar))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(srs => srs.Email))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(srs => srs.DateOfBirth))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(srs => srs.FullName))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(srs => srs.Location));
        }
    }
}
