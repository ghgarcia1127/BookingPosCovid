using PostCovidBooking.Core.Dto;
using PostCovidBooking.Data.Models;
using System;
using System.Threading.Tasks;

namespace PostCovidBooking.Core.Interfaces
{
    public interface IReservationService : IService<Reservation, ReservationDTO>
    {
        Task<bool> CreateReservation(ReservationDTO reservation);
        Task<bool> ValidateAvailabilityAsync(DateTime initialDate, DateTime finalDate);
    }
}
