using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurfBoardss.Server.Data;
using SurfBoardss.Shared;

namespace SurfBoardss.Server.Controllers {

    [ApiVersion("2")]
    [Route("api/v{version:apiVersion}/surfboards")]
    [ApiController]
    public class SurfboardsV2Controller : ControllerBase {
        private readonly Surf_BoardsContext _context;

        public SurfboardsV2Controller(Surf_BoardsContext context) {
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

        [HttpPost("updateboard/{id}")]
        public async Task<IActionResult> Edit(Guid id, [FromBody] SurfBoard surfboards)
        {
            try
            {
                _context.Update(surfboards);
                await _context.SaveChangesAsync();
                return Ok(surfboards);
            }

            catch (Exception e)
            {
                return Conflict(e.Message);
            }
        }

    }
}
