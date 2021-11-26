using PostCovidBooking.Infraestructure.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace PostCovidBooking.Core.Dto
{
    public class ReservationDTO
    {
        [Required]
        public Guid Id { get; set; }
        [SmallerThanDate("Today", 30)]
        [GreaterThanDate("Today", 1)]
        public DateTime InitialDate { get; set; }
        [SmallerThanDate("InitialDate", 3)]
        public DateTime FinalDate { get; set; }
        public string Email { get; set; }
    }
}
