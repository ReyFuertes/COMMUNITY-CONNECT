using AutoMapper;
using BookingService.Application.Dtos;
using BookingService.Domain.Entities;

namespace BookingService.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Amenity, AmenityDto>().ReverseMap();
            CreateMap<Booking, BookingDto>()
                .ForMember(dest => dest.AmenityName, opt => opt.MapFrom(src => src.Amenity!.Name));
            CreateMap<BookingDto, Booking>();
            CreateMap<BookingRule, BookingRuleDto>().ReverseMap();
        }
    }
}