using AutoMapper;
using PostCovidBooking.Core.Dto;
using PostCovidBooking.Data.Models;

namespace PostCovidBooking
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ReservationDTO, Reservation>().ReverseMap();
        }
    }
}
