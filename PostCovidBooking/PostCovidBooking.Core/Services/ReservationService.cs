using AutoMapper;
using PostCovidBooking.Core.Dto;
using PostCovidBooking.Core.Interfaces;
using PostCovidBooking.Data.Interfaces;
using PostCovidBooking.Data.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PostCovidBooking.Core.Services
{
    public class ReservationService : Service<Reservation, ReservationDTO>, IReservationService 
    {
        private readonly IReservationRepository repository;
        public ReservationService(IReservationRepository repository, IMapper mapper)
            : base(repository, mapper) => this.repository = repository;

        public async Task<bool> CreateReservation(ReservationDTO reservation)
        {
            var availability = await ValidateAvailabilityAsync(reservation.InitialDate, reservation.FinalDate).ConfigureAwait(false);
            if (availability)
            {
                try
                {
                    await AddAsync(reservation).ConfigureAwait(false);
                }
                catch
                {
                    return false;
                }
            }
            return availability;
        }

        public async Task<bool> ValidateAvailabilityAsync(DateTime initialDate, DateTime finalDate)
        {

            var availability = await GetAllAsync(reservation => reservation.InitialDate.Equals(initialDate) 
                                                             || reservation.FinalDate.Equals(finalDate)
                                                             || (initialDate > reservation.InitialDate && initialDate < reservation.FinalDate)).ConfigureAwait(false);

            return !availability?.Any() ?? false;
        }
    }
}
