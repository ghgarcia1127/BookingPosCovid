using PostCovidBooking.Data.Interfaces;
using PostCovidBooking.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PostCovidBooking.Data.Repositories
{
    public class ReservationRepository : BaseRepository<Reservation>, IReservationRepository
    {
        public ReservationRepository(IQueryableUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
