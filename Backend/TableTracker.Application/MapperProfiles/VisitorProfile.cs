using AutoMapper;

using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Entities;

namespace TableTracker.Application.MapperProfiles
{
    public class VisitorProfile : Profile
    {
        public VisitorProfile()
        {
            CreateMap<Visitor, VisitorDTO>()
                .ForMember(dest => dest.Reservations, opt => opt.MapFrom(src => src.Reservations))
                .ForMember(dest => dest.Favourites, opt => opt.MapFrom(src => src.Favourites))
                .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => src.Avatar));

            CreateMap<VisitorDTO, Visitor>()
                .ForMember(dest => dest.Reservations, opt => opt.MapFrom(src => src.Reservations))
                .ForMember(dest => dest.Favourites, opt => opt.MapFrom(src => src.Favourites))
                .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => src.Avatar));
        }
    }
}
