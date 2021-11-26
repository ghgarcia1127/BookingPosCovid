using System;
using System.Collections.Generic;
using System.Text;

namespace PostCovidBooking.Models
{
    public class Reservation
    {
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishData { get; set; }
        public string UserName { get; set; }
    }
}
