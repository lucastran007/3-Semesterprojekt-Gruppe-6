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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Surfboard,StartTime,EndTime,User")] Rental rental)
        {
            var user = await _userManager.GetUserAsync(User);

            if (ModelState.IsValid)
            {
                //Generate unique ID
                rental.Id = Guid.NewGuid();
                //Gets current user
             


                _context.Add(rental);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");


            }
            return View();

        }

    }
}
