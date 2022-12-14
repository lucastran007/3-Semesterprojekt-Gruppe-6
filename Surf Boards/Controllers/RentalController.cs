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
        private readonly SignInManager<Surf_BoardsUser> _signInManager;

        public RentalController(Surf_BoardsContext context, UserManager<Surf_BoardsUser> userManager, SignInManager<Surf_BoardsUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
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
            // Not signed in, return a form view
            if (!_signInManager.IsSignedIn(User))
            {
                return View("Form");
            } else
            {
                // Signed in
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Guid id, string rentalName, string rentalPhone, DateTime rentalDate)
        {

            if(_signInManager.IsSignedIn(User))
            {
                var board = await _context.SurfBoard.FindAsync(id);
                var user = await _userManager.GetUserAsync(User);
                rentalName = user.FirstName + " " + user.LastName;
                rentalPhone = user.PhoneNumber;
                //var userIp = HttpContext.Connection.RemoteIpAddress.ToString();
                var userIp = "123.123.123.123";

                // Look up if IP has X records already
                var rentals = _context.Rental;
                int amountOfRentalsFromIp = rentals.Count(r => r.UserIp == userIp);

                // Checks succeeded, create new rental
                Guid rentalId = Guid.NewGuid();
                Rental rental = new Rental(rentalId, rentalName, rentalPhone, rentalDate, id, user.Id, userIp);

                if (ModelState.IsValid)
                {
                    _context.Add(rental);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Udlejning oprettet ✅";
                    return RedirectToAction("Index");
                }
                return View();

            } else {

                var board = await _context.SurfBoard.FindAsync(id);
                //var userIp = HttpContext.Connection.RemoteIpAddress.ToString();
                var userIp = "123.123.123.123";

                // Look up if IP has X records already
                var rentals = _context.Rental;
                int amountOfRentalsFromIp = rentals.Count(r => r.UserIp == userIp);

                if (amountOfRentalsFromIp >= 3)
                {
                    TempData["WarningMessage"] = "Du har nået max antal af udlejninger. Log venligst ind hvis du ønsker at leje flere boards!";
                    return RedirectToAction("Index", "Home");
                }

                // Checks succeeded, create new rental
                Guid rentalId = Guid.NewGuid();
                Rental rental = new Rental(rentalId, rentalName, rentalPhone, rentalDate, id, null, userIp);

                if (ModelState.IsValid)
                {
                    _context.Add(rental);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Udlejning oprettet ✅";
                    return RedirectToAction("Index", "Home");
                }
                return RedirectToAction("Index", "Home");

            }

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid rentalId)
        {
            if (_context.Rental == null)
            {
                return Problem("Entity set 'Surf_BoardsContext.Rental'  is null.");
            }
            var rental = await _context.Rental.FindAsync(rentalId);
            
            _context.Rental.Remove(rental);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Udlejning slettet ✅";
            return RedirectToAction(nameof(Index));
        }

        //private bool RentalExists(Guid rentalId)
        //{
        //    return _context.Rental.Any(e => e.RentalId == rentalId);
        //}

    }
}
