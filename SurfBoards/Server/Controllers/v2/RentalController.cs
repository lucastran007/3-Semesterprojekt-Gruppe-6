using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurfBoards.Server.Data;
using SurfBoards.Shared;

namespace SurfBoards.Server.Controllers
{
    [ApiVersion("2")]
    [Route("api/v{version:apiVersion}/rental")]
    [ApiController]
    public class RentalV2Controller : ControllerBase
    {
        private readonly Surf_BoardsContext _context;
        public RentalV2Controller(Surf_BoardsContext context)
        {
            _context = context;
        }

        // Get all Rentals 
        [HttpGet]
        public async Task<IActionResult> GetRentals()
        { 
            var rentals = await _context.Rental.ToArrayAsync();
            return Ok(rentals);
        }

        // Get rental by GUID Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRental(Guid id)
        {
            var rental = await _context.Rental.FindAsync(id);
            if (rental == null)
                return NotFound();

            return Ok(rental);
        }


        [HttpPost]
        public async Task<IActionResult> AddRental(Rental rental)
        {
            _context.Rental.Add(rental);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRental",
                new { id = rental.RentalId },
                rental);
        }

        [HttpPut("{id}")]
        public async Task <IActionResult> UpdateRental(Guid id, [FromBody] Rental rental)
        {
             if (id != rental.RentalId)
            {
                return BadRequest();
            }

             _context.Entry(rental).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                if (!_context.Rental.Any(r => r.RentalId == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok(rental);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRental([FromBody] Guid id)
        {
            var rentals = await _context.Rental.FindAsync(id);
            if (rentals != null)
            {
                _context.Remove(rentals);
                await _context.SaveChangesAsync();
                return Ok(rentals);
            }
            return Ok(NotFound());
        }

    }
}
