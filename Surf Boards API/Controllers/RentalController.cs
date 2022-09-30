using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Surf_Boards.Data;
using Surf_Boards_API.Models;

namespace Surf_Boards_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalController : ControllerBase
    {
        private readonly Surf_BoardsContext _context;
        public RentalController(Surf_BoardsContext context)
        {
            _context = context;
        }
        // Get all Rentals 
        [HttpGet]
        public async Task<IActionResult> GetRentals()
        { var rentals = await _context.Rental.ToArrayAsync();
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
    }
}
