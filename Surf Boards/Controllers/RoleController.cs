using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using static Surf_Boards.Core.ConstantsRole;

namespace Surf_Boards.Controllers
{
    public class RoleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Manager()
        {
            return View();
        }

       
        [Authorize(Roles = $"{Roles.Administrator}")]
        public IActionResult Admin()
        {
            return View();
        }
    }
}
