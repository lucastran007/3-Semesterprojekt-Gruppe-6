using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Surf_Boards.Areas.Identity.Data;
using Surf_Boards.Core.Repository;
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

        public IActionResult Index()
        {

            return View();
        }

        //get Rental/create
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Guid id)
        {

            var board = await _context.SurfBoard.FindAsync(id);
            var user = await _userManager.GetUserAsync(User);
            Guid rentalId = Guid.NewGuid();
            Rental rental = new Rental(rentalId, DateTime.Now, id, user.Id);
            
            if(ModelState.IsValid) { 
                _context.Add(rental);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View();

        }

    }
}
