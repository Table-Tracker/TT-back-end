using AutoMapper;

using TableTracker.Domain.DataTransferObjects;
using TableTracker.Domain.Entities;

namespace TableTracker.Application.MapperProfiles
{
    public class ImageProfile : Profile
    {
        public ImageProfile()
        {
            CreateMap<Image, ImageDTO>();

            CreateMap<ImageDTO, Image>();
        }
    }
}
