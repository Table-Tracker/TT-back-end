using AutoMapper;

using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Entities;

namespace TableTracker.Application.MapperProfiles
{
    public class FranchiseProfile : Profile
    {
        public FranchiseProfile()
        {
            CreateMap<Franchise, FranchiseDTO>()
                .ForMember(dest => dest.Restaurants, opt => opt.MapFrom(src => src.Restaurants));

            CreateMap<FranchiseDTO, Franchise>()
                .ForMember(dest => dest.Restaurants, opt => opt.MapFrom(src => src.Restaurants));
        }
    }
}
