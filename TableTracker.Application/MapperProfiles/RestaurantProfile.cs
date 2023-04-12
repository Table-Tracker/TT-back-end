using AutoMapper;

using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Entities;

namespace TableTracker.Application.MapperProfiles
{
    public class RestaurantProfile : Profile
    {
        public RestaurantProfile()
        {
            CreateMap<Restaurant, RestaurantDTO>()
               .ForMember(dest => dest.Franchise, opt => opt.MapFrom(src => src.Franchise))
               .ForMember(dest => dest.Layout, opt => opt.MapFrom(src => src.Layout))
               .ForMember(dest => dest.Cuisines, opt => opt.MapFrom(src => src.Cuisines))
               .ForMember(dest => dest.Tables, opt => opt.MapFrom(src => src.Tables));

            CreateMap<RestaurantDTO, Restaurant>()
                .ForMember(dest => dest.Franchise, opt => opt.MapFrom(src => src.Franchise))
                .ForMember(dest => dest.FranchiseId, opt => opt.MapFrom(src => src.Franchise.Id))
                .ForMember(dest => dest.Layout, opt => opt.MapFrom(src => src.Layout))
                .ForMember(dest => dest.LayoutId, opt => opt.MapFrom(src => src.Layout.Id))
                .ForMember(dest => dest.Cuisines, opt => opt.MapFrom(src => src.Cuisines))
                .ForMember(dest => dest.MainImage, opt => opt.MapFrom(src => src.MainImage))
                .ForMember(dest => dest.Tables, opt => opt.MapFrom(src => src.Tables));
        }
    }
}
