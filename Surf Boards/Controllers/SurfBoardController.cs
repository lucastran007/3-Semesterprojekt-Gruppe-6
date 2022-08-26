using Microsoft.AspNetCore.Mvc;

namespace Surf_Boards.Controllers
{
    public class SurfBoardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SurfBoard()
        {
            return View();
        }
    }
}
