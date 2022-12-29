using Blazor.Server.Data;
using Blazor.Shared.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Blazor.Shared;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public RentalController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Get all Rentals 
        [HttpGet]
        public async Task<IActionResult> GetRentals()
        {
            if (User.IsInRole("Administrator"))
            {
                return Ok(await _context.Rental.ToArrayAsync());
            }
            else
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    return Ok(await _context.Rental.Where(r => r.UserId == user.Id).ToListAsync());
                }
                return Ok(new List<Rental>());
            }
        }

        // Get rental by GUID Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRental(Guid id)
        {
            var rental = await _context.Rental.FindAsync(id);
            if (rental == null) return NotFound();
            
            return Ok(rental);
        }

        [HttpPost]
        public ActionResult<Rental> CreateRental(Rental rental)
        {
            _context.Rental.Add(rental);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetRental), new { id=rental.RentalId}, rental);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRental(Guid id)
        {
            var rental = await _context.Rental.FindAsync(id);
            _context.Rental.Remove(rental);
            _context.SaveChanges();
            return Ok();
        }

    }
}
