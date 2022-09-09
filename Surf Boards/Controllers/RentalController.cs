using Microsoft.AspNetCore.Mvc;

namespace Surf_Boards.Controllers
{
    public class RentalController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
