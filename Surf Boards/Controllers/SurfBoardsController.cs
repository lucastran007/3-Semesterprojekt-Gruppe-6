using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Surf_Boards.Data;
using Surf_Boards.Models;
using Microsoft.AspNetCore.Hosting;
using System.Diagnostics;

namespace Surf_Boards.Controllers
{
    public class SurfBoardsController : Controller
    {
        private readonly Surf_BoardsContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public SurfBoardsController(Surf_BoardsContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
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
        public async Task<IActionResult> Create([Bind("Id,BoardName,Length,Width,Thickness,Volume,Boardtype,Price,Equipment,ImageFile")] SurfBoard surfBoard)
        {
            if (ModelState.IsValid)
            {


                surfBoard.Id = Guid.NewGuid();
                if (surfBoard.ImageFile != null)
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string filename = Path.GetFileNameWithoutExtension(surfBoard.ImageFile.FileName);
                    string extension = Path.GetExtension(surfBoard.ImageFile.FileName);
                    surfBoard.ImageName = filename = filename + "-" + surfBoard.Id +  extension;
                    string path = Path.Combine(wwwRootPath + "/Images/", filename);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await surfBoard.ImageFile.CopyToAsync(fileStream);
                    }

                }

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
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,BoardName,Length,Width,Thickness,Volume,Boardtype,Price,Equipment,ImageFile")] SurfBoard surfBoard)
        {
            if (id != surfBoard.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                try
                {

                    if (surfBoard.ImageFile != null)
                    {
                        var ip = await _context.SurfBoard.FindAsync(id);
                        string imagePathe = _hostEnvironment.WebRootPath;
                        if (ip.ImageName != null)
                        {
                            var imagePath = Path.Combine(imagePathe + "/Images/", ip.ImageName);
                            if (System.IO.File.Exists(imagePath))
                                System.IO.File.Delete(imagePath);
                            _context.ChangeTracker.Clear();
                            await _context.SaveChangesAsync();

                        }

                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string filename = Path.GetFileNameWithoutExtension(surfBoard.ImageFile.FileName);
                        string extension = Path.GetExtension(surfBoard.ImageFile.FileName);
                        surfBoard.ImageName = filename = filename + "-" + surfBoard.Id + extension;
                        string path = Path.Combine(wwwRootPath + "/Images/", filename);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await surfBoard.ImageFile.CopyToAsync(fileStream);
                        }
                    }

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
                if (surfBoard.ImageName != null)
                {
                    var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "Images", surfBoard.ImageName);
                    if (System.IO.File.Exists(imagePath))
                        System.IO.File.Delete(imagePath);
                }
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
