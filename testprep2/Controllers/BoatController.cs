using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Context;
using Models;
using DTOs;

namespace Controllers;

[Route("/api/[controller]")]
[ApiController]
public class BoatController : ControllerBase
{
    private readonly BoatDbContext _context;

    public BoatController(BoatDbContext context)
    {
        _context = context;
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClientCategory>>> GetClientCategories()
    {
        return await _context.ClientCategories.OrderBy(c => c.Name).ToListAsync();
    }
    [HttpGet("/reservations/{customerId}")]
    public async Task<ActionResult> GetCustomerReservations(int customerId)
    {
        var customerReservations = await _context.Clients
            .Include(c => c.Reservations)
            .Where(c => c.IdClient == customerId)
            .FirstOrDefaultAsync();

        if (customerReservations == null)
        {
            return NotFound();
        }
        return Ok(customerReservations);
    }

    public async Task<IActionResult> CreateReservationAsync(Reservation reservation)
    {
        await _context.Reservations.AddAsync(reservation);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetReservation", new { id = reservation.IdReservation }, reservation);
    }
}