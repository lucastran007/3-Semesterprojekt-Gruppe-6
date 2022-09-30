using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Surf_Boards.Data;

namespace Surf_Boards_API.Controllers {

    [Route("api/[controller]")]
    [ApiController]

    public class SurfboardsController : ControllerBase {
        private readonly Surf_BoardsContext _context;

        public SurfboardsController(Surf_BoardsContext context) {
            _context = context;
        }
    

        // GET ALL SURFBOARDS
        [HttpGet]
        public async Task<ActionResult> GetAllSurfBoards() {
            var surfboards = await _context.SurfBoard.ToArrayAsync();
            return Ok(surfboards);
        }

        // GET SURFBOARD BY GUID
        [HttpGet("{id}")]
        public async Task<ActionResult> GetSurfBoard(Guid id) {
            var surfboard = await _context.SurfBoard.FindAsync(id);
            if (surfboard == null)
                return NotFound();

            return Ok(surfboard);
        }

    }
}
