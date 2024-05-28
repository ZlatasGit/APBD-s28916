using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using lab8.Context;
using lab8.Models;

namespace lab8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly ITripService _tripService;

        public TripsController(ITripService tripService)
        {
            _tripService = tripService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTrips([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var (trips, totalPages) = await _tripService.GetTripsAsync(page, pageSize);
            return Ok(new { pageNum = page, pageSize, allPages = totalPages, trips });
        }

        [HttpDelete("{idClient}")]
        public async Task<IActionResult> DeleteClient(int idClient)
        {
            var result = await _tripService.DeleteClientAsync(idClient);
            if (!result)
            {
                return BadRequest("Client has assigned trips or does not exist.");
            }
            return NoContent();
        }

        [HttpPost("{idTrip}/clients")]
        public async Task<IActionResult> AssignClientToTrip(int idTrip, [FromBody] ClientDto clientDto)
        {
            var result = await _tripService.AssignClientToTripAsync(idTrip, clientDto);
            if (!result)
            {
                return BadRequest("Error in assigning client to trip.");
            }
            return NoContent();
        }
    }
}