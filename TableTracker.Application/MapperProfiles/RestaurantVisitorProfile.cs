using AutoMapper;

using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Entities;

namespace TableTracker.Application.MapperProfiles
{
    public class RestaurantVisitorProfile : Profile
    {
        public RestaurantVisitorProfile()
        {
            CreateMap<RestaurantVisitor, RestaurantVisitorDTO>()
                .ForMember(dest => dest.Restaurant, opt => opt.MapFrom(src => src.Restaurant))
                .ForMember(dest => dest.Visitor, opt => opt.MapFrom(src => src.Visitor));

            CreateMap<RestaurantVisitorDTO, RestaurantVisitor>()
                .ForMember(dest => dest.Visitor, opt => opt.MapFrom(src => src.Visitor))
                .ForMember(dest => dest.VisitorId, opt => opt.MapFrom(src => src.Visitor.Id))
                .ForMember(dest => dest.Restaurant, opt => opt.MapFrom(src => src.Restaurant))
                .ForMember(dest => dest.RestaurantId, opt => opt.MapFrom(src => src.Restaurant.Id));
        }
    }
}
