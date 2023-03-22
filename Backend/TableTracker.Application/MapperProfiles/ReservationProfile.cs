using AutoMapper;

using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Entities;

namespace TableTracker.Application.MapperProfiles
{
    public class ReservationProfile : Profile
    {
        public ReservationProfile()
        {
            CreateMap<Reservation, ReservationDTO>()
                .ForMember(dest => dest.Visitor, opt => opt.MapFrom(src => src.Visitor))
                .ForMember(dest => dest.Table, opt => opt.MapFrom(src => src.Table));

            CreateMap<ReservationDTO, Reservation>()
                .ForMember(dest => dest.Visitor, opt => opt.MapFrom(src => src.Visitor))
                .ForMember(dest => dest.VisitorId, opt => opt.MapFrom(src => src.Visitor.Id))
                .ForMember(dest => dest.Table, opt => opt.MapFrom(src => src.Table))
                .ForMember(dest => dest.TableId, opt => opt.MapFrom(src => src.Table.Id));
        }
    }
}
