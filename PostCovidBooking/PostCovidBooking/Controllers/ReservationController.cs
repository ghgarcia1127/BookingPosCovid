using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PostCovidBooking.Core;
using PostCovidBooking.Core.Dto;
using PostCovidBooking.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostCovidBooking.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReservationController : ControllerBase
    {

        private readonly ILogger<ReservationController> _logger;
        private readonly IReservationService service;

        public ReservationController(ILogger<ReservationController> logger, IReservationService service)
        {
            _logger = logger;
            this.service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservationDTO>>> GetAll()
        {
            var reservations = await service.GetAllAsync().ConfigureAwait(false);

            if(!reservations?.Any() ?? true)
            {
                return NoContent();
            }
            return Ok(reservations);
        }

        [HttpGet("{email}")]
        public async Task<ActionResult<IList<ReservationDTO>>> GetReservation(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return BadRequest();
            }

            var reservation = await service.GetAllAsync(reservation => reservation.Email.Equals(email)).ConfigureAwait(false);

            if(reservation == null)
            {
                return NoContent();
            }

            return Ok(reservation);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservation(Guid id, ReservationDTO reservation)
        {
            if (!id.Equals(reservation.Id))
            {
                return BadRequest();
            }

            await service.UpdateAsync(reservation).ConfigureAwait(false);

            return Accepted();
        }

        [HttpDelete]
        public async Task<ActionResult<ReservationDTO>> DeleteReservation(ReservationDTO reservation)
        {
            if (reservation == null)
            {
                return BadRequest();
            }

            await service.DeleteAsync(reservation).ConfigureAwait(false);

            return Accepted();
        }

        [HttpPost]
        public async Task<IActionResult> CreateReservation(ReservationDTO reservation)
        {
            if(!await service.CreateReservation(reservation).ConfigureAwait(false))
            {
                return BadRequest("Reservation couldn't be created");
            }

            return Accepted();
        }

        [HttpPost("Availability")]
        public async Task<ActionResult<bool>> ValidateAvailability(ReservationDTO reservation)
        {
            var availability = await service.ValidateAvailabilityAsync(reservation.InitialDate, reservation.FinalDate).ConfigureAwait(false);

            return Ok(availability);
        }
    }
}
