using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Surf_Boards.Areas.Identity.Data;
using Surf_Boards.Core.Repository;
using Surf_Boards.Core.ViewModel;
using Surf_Boards.Data;
using Surf_Boards.Models;
using Surf_Boards.Models.Domain;

namespace Surf_Boards.Controllers
{
    public class RentalController : Controller
    {
        private readonly Surf_BoardsContext _context;
        private readonly UserManager<Surf_BoardsUser> _userManager;

        public RentalController(Surf_BoardsContext context, UserManager<Surf_BoardsUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = await _userManager.GetUserIdAsync(user);

            // Get all rentals by userId
            var rentals = _context.Rental
              .Where(x => x.UserId == userId)
              .Select(n => new {
                  n.RentalId,
                  n.SurfboardId,
                  n.RentalDate
              });
            //_context.ChangeTracker.Clear();
            // Create a new list of boards
            List<SurfBoard> rentedBoards = new List<SurfBoard>();
            List<Rental> userRentals = new List<Rental>(); 

            // Loop through each rental and add it to the newly created surfboard list
            foreach (var rental in rentals)
            {

                rentedBoards.Add(_context.SurfBoard.Find(rental.SurfboardId));
                userRentals.Add(_context.Rental.Find(rental.RentalId));

            }

            // Create the viewmodel
            var rentalIndexVM = new RentalIndexViewModel
            {
                Surfboards = rentedBoards,
                Rentals = userRentals
            };

            return View(rentalIndexVM);
        }

        //get Rental/create
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Guid id, DateTime rentalDate)
        {

            var board = await _context.SurfBoard.FindAsync(id);
            var user = await _userManager.GetUserAsync(User);
            Guid rentalId = Guid.NewGuid();
            Rental rental = new Rental(rentalId, rentalDate, id, user.Id);
            
            if(ModelState.IsValid) { 
                _context.Add(rental);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View();

        }

        // GET: SurfBoards/Delete/5
        public async Task<IActionResult> Delete(Guid? rentalId)
        {
            if (rentalId == null || _context.Rental == null)
            {
                return NotFound();
            }

            var rental = await _context.Rental
                .FirstOrDefaultAsync(m => m.RentalId == rentalId);
            if (rental == null)
            {
                return NotFound();
            }

            return View(rental);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid rentalId)
        {
            if (_context.Rental == null)
            {
                return Problem("Entity set 'Surf_BoardsContext.Rental'  is null.");
            }
            var rental = await _context.Rental.FindAsync(rentalId);
            
            _context.Rental.Remove(rental);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RentalExists(Guid rentalId)
        {
            return _context.Rental.Any(e => e.RentalId == rentalId);
        }

    }
}
