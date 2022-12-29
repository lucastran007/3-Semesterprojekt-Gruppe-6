using Blazor.Server.Data;
using Blazor.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Server.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SurfboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SurfboardController(ApplicationDbContext context)
        {
            _context = context;
        }


        // GET ALL SURFBOARDS
        [HttpGet]
        public ActionResult<List<SurfBoard>> GetAllSurfBoards()
        {
            //var surfboards = await _context.SurfBoard.ToArrayAsync();
            return _context.SurfBoard.ToList();
        }

        // GET SURFBOARD BY GUID
        [HttpGet("{id}")]
        public async Task<ActionResult<SurfBoard>> GetSurfBoard(Guid id)
        {
            var surfboard = await _context.SurfBoard.FindAsync(id);
            if (surfboard == null) return NotFound();

            return surfboard;
        }

        //Create new surfboard (post it to the server)
        [HttpPost]
        public ActionResult<SurfBoard> CreateSurfBoard(SurfBoard surfBoard)
        {
            _context.SurfBoard.Add(surfBoard);
            _context.SaveChanges();
            return Ok();
        }


        //Delete a surfboard
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSurfboard(Guid id)
        {
            var surfBoard = await _context.SurfBoard.FindAsync(id);
            if (surfBoard == null) return NotFound();

            _context.SurfBoard.Remove(surfBoard);
            _context.SaveChanges();
            return Ok();
        }
            
    }
}
