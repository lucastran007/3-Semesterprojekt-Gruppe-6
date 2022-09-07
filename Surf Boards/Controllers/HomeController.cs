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


        public async Task<IActionResult> Index(string SearchString, string sortOrder, string currentFilter, int? pageNumber)
        {
           
            //Searchning 
            ViewData["CurrentFilter"] = SearchString;
            if (SearchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                SearchString = currentFilter;
            }
            var surfBoard = from s in _context.SurfBoard
                            select s;

            if (!String.IsNullOrEmpty(SearchString))
            {
                surfBoard = surfBoard.Where(s => s.BoardName.Contains(SearchString));
            }
            //Sorting 
            ViewData["NameSort"] = String.IsNullOrEmpty(sortOrder) ? "BoardName_desc" : "";
            ViewData["LengthSort"] = sortOrder == "Length" ? "Length_desc" : "Length";
            ViewData["WidthSort"] = sortOrder == "Width" ? "Width_desc" : "Width";
            ViewData["ThicknessSort"] = sortOrder == "Thickness" ? "Thickness_desc" : "Thickness";
            ViewData["VolumeSort"] = sortOrder == "Volume" ? "Volume_desc" : "Volume";
            ViewData["PriceSort"] = sortOrder == "Price" ? "Price_desc" : "Price";
            ViewData["BoardTypeSort"] = sortOrder == "BoardType" ? "BoardType_desc" : "BoardType";
    
            switch (sortOrder)
            {
                case "BoardName_desc":
                    surfBoard= surfBoard.OrderByDescending(s => s.BoardName);
                    break;
                case "Length":
                    surfBoard = surfBoard.OrderBy(s => s.Length);
                    break;
                case "Length_desc":
                    surfBoard = surfBoard.OrderByDescending(s => s.Length);
                    break;
                case "Width":
                    surfBoard = surfBoard.OrderByDescending(s => s.Width);
                    break;
                case "Width_desc":
                    surfBoard = surfBoard.OrderBy(s => s.Width);
                    break;
                case "Thickness":
                    surfBoard = surfBoard.OrderByDescending(s => s.Thickness);
                    break;
                case "Thickness_desc":
                    surfBoard = surfBoard.OrderBy(s => s.Thickness);
                    break;
                case "Volume":
                    surfBoard = surfBoard.OrderByDescending(s => s.Volume);
                    break;
                case "Volume_desc":
                    surfBoard = surfBoard.OrderBy(s => s.Volume);
                    break;
                case "Price":
                    surfBoard = surfBoard.OrderByDescending(s => s.Price);
                    break;
                case "Price_desc":
                    surfBoard = surfBoard.OrderBy(s => s.Price);
                    break;
                case "BoardType":
                    surfBoard = surfBoard.OrderByDescending(s => s.Price);
                    break;
                case "BoardType_desc":
                    surfBoard = surfBoard.OrderBy(s => s.Price);
                    break;
                default:
                    surfBoard=surfBoard.OrderBy(s => s.BoardName);
                    break;
            }
            //Pagination 
            ViewData["CurrentSort"] = sortOrder;
            int pageSize = 10;
            return View(await PaginatedList<SurfBoard>.CreateAsync(surfBoard.AsNoTracking(), pageNumber?? 1, pageSize));
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