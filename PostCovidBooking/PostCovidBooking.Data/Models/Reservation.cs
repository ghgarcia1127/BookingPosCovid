using Newtonsoft.Json;
using System;

namespace PostCovidBooking.Data.Models
{
    public class Reservation
    {
        public Guid Id { get; set; }
        public DateTime InitialDate { get; set; }
        public DateTime FinalDate { get; set; }
        public string Email { get; set; }
    }
}
