using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Surf_Boards.Data;
using Surf_Boards.Models;

namespace Surf_Boards.Controllers
{
    public class SurfBoardsController : Controller
    {
        private readonly Surf_BoardsContext _context;

        public SurfBoardsController(Surf_BoardsContext context)
        {
            _context = context;
        }

        // GET: SurfBoards
        public async Task<IActionResult> Index()
        {
              return View(await _context.SurfBoard.ToListAsync());
        }

        // GET: SurfBoards/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.SurfBoard == null)
            {
                return NotFound();
            }

            var surfBoard = await _context.SurfBoard
                .FirstOrDefaultAsync(m => m.Id == id);
            if (surfBoard == null)
            {
                return NotFound();
            }

            return View(surfBoard);
        }

        // GET: SurfBoards/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SurfBoards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BoardName,Length,Width,Thickness,Volume,Boardtype,Price,Equipment")] SurfBoard surfBoard)
        {
            if (ModelState.IsValid)
            {
                surfBoard.Id = Guid.NewGuid();
                _context.Add(surfBoard);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(surfBoard);
        }

        // GET: SurfBoards/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.SurfBoard == null)
            {
                return NotFound();
            }

            var surfBoard = await _context.SurfBoard.FindAsync(id);
            if (surfBoard == null)
            {
                return NotFound();
            }
            return View(surfBoard);
        }

        // POST: SurfBoards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,BoardName,Length,Width,Thickness,Volume,Boardtype,Price,Equipment")] SurfBoard surfBoard)
        {
            if (id != surfBoard.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(surfBoard);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SurfBoardExists(surfBoard.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(surfBoard);
        }

        // GET: SurfBoards/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.SurfBoard == null)
            {
                return NotFound();
            }

            var surfBoard = await _context.SurfBoard
                .FirstOrDefaultAsync(m => m.Id == id);
            if (surfBoard == null)
            {
                return NotFound();
            }

            return View(surfBoard);
        }

        // POST: SurfBoards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.SurfBoard == null)
            {
                return Problem("Entity set 'Surf_BoardsContext.SurfBoard'  is null.");
            }
            var surfBoard = await _context.SurfBoard.FindAsync(id);
            if (surfBoard != null)
            {
                _context.SurfBoard.Remove(surfBoard);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SurfBoardExists(Guid id)
        {
          return _context.SurfBoard.Any(e => e.Id == id);
        }
    }
}
