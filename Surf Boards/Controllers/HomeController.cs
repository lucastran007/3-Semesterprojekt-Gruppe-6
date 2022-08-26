using Microsoft.AspNetCore.Mvc;
using Surf_Boards.Models;
using System.Diagnostics;
using Surf_Boards.Data;
using Microsoft.EntityFrameworkCore;


namespace Surf_Boards.Controllers
{
    public class HomeController : Controller
    {
        private readonly Surf_BoardsContext _context;

        public HomeController(Surf_BoardsContext context)
        {
            _context = context;
        }

        // GET: SurfBoards


        public async Task<IActionResult> Index()
        {
            var surfBoard = await _context.SurfBoard.ToListAsync();
            return View(surfBoard);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}