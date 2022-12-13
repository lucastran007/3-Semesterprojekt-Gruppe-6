using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Surf_Boards.Data;
using Surf_Boards.Models;
using Microsoft.AspNetCore.Authorization;
using Surf_Boards.Core;

namespace Surf_Boards.Controllers
{
    [Authorize(Roles = $"{ConstantsRole.Roles.Administrator},{ConstantsRole.Roles.Manager}")]
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
                    surfBoard = surfBoard.OrderByDescending(s => s.BoardName);
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
                    surfBoard = surfBoard.OrderBy(s => s.BoardName);
                    break;
            }
            //Pagination 
            ViewData["CurrentSort"] = sortOrder;
            int pageSize = 10;
            return View(await PaginatedList<SurfBoard>.CreateAsync(surfBoard.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: SurfBoards/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.SurfBoard == null)
            {
                return NotFound();
            }

            var surfBoard = await _context.SurfBoard
                .AsNoTracking()
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

                // generate unique id
                surfBoard.Id = Guid.NewGuid();
                //image stuff
                //save image to wwwroot/images
                if (surfBoard.ImageFile != null)
                {
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
                // Her Adder vi lortet 
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

            var surfBoard = await _context.SurfBoard
           .AsNoTracking()
           .FirstOrDefaultAsync(m => m.Id == id);

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
        public async Task<IActionResult> Edit(Guid id, byte[] rowVersion, [Bind("Id,BoardName,Length,Width,Thickness,Volume,Boardtype,Price,Equipment,ImageName,ImageFile")] SurfBoard surfBoard)
        {
            if (id != surfBoard.Id)
            {
                return NotFound();
            }

            if (surfBoard == null)
            {
                SurfBoard surboard = new SurfBoard();
                await TryUpdateModelAsync(surboard);
                ModelState.AddModelError(string.Empty, "unable to save, item deletede by other user");
                return View(surboard);
            }

            if (ModelState.IsValid)
            {
                _context.Entry(surfBoard).Property("RowVersion").OriginalValue = rowVersion;

                try
                {

                    // Does the user have added a new image? Then we replace the old 
                    if (surfBoard.ImageFile != null && surfBoard.ImageFile.Length > 0)
                    {
                        // It check if there is an image and if so deletes it 
                        if (surfBoard.ImageName != null && surfBoard.ImageFile != null)
                        {
                            string imagePathe = _hostEnvironment.WebRootPath;
                            var imagePath = Path.Combine(imagePathe + "/Images/", surfBoard.ImageName);
                            if (System.IO.File.Exists(imagePath))
                                System.IO.File.Delete(imagePath);
                            _context.ChangeTracker.Clear();
                            await _context.SaveChangesAsync();

                        }
                        else
                        {
                            _context.ChangeTracker.Clear();
                        }
                        // saves a new image 
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
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    var exceptionEntry = ex.Entries.Single();
                    var clientValues = (SurfBoard)exceptionEntry.Entity;
                    var databaseEntry = exceptionEntry.GetDatabaseValues();
                    if (databaseEntry == null)
                    {
                        ModelState.AddModelError(string.Empty,
                            "Unable to save changes. The surfboard was deleted by another user.");
                    }
                    else
                    {
                        var databaseValues = (SurfBoard)databaseEntry.ToObject();

                        if (databaseValues.BoardName != clientValues.BoardName)
                        {
                            ModelState.AddModelError("Name", $"Current value: {databaseValues.BoardName}");
                        }
                        if (databaseValues.Length != clientValues.Length)
                        {
                            ModelState.AddModelError("lenght", $"Current value: {databaseValues.Length}");
                        }
                        if (databaseValues.Width != clientValues.Width)
                        {
                            ModelState.AddModelError("Width", $"Current value: {databaseValues.Width}");
                        }
                        if (databaseValues.Thickness != clientValues.Thickness)
                        {
                            ModelState.AddModelError("Thickness", $"Current value: {databaseValues.Thickness}");
                        }
                        if (databaseValues.Volume != clientValues.Volume)
                        {
                            ModelState.AddModelError("Volume", $"Current value: {databaseValues.Volume}");
                        }
                        if (databaseValues.Boardtype != clientValues.Boardtype)
                        {
                            ModelState.AddModelError("Type", $"Current value: {databaseValues.Boardtype}");
                        }
                        if (databaseValues.Price != clientValues.Price)
                        {
                            ModelState.AddModelError("price", $"Current value: {databaseValues.Price}");
                        }
                        if (databaseValues.Equipment != clientValues.Equipment)
                        {
                            ModelState.AddModelError("Equipment", $"Current value: {databaseValues.Equipment}");
                        }

                        ModelState.AddModelError(string.Empty, "The record you attempted to edit "
                                + "was modified by another user after you got the original value. The "
                                + "edit operation was canceled and the current values in the database "
                                + "have been displayed. If you still want to edit this record, click "
                                + "the Save button again. Otherwise click the Back to List hyperlink.");
                        surfBoard.RowVersion = (byte[])databaseValues.RowVersion;
                        ModelState.Remove("RowVersion");
                    }

                }
            }
            return View(surfBoard);
        }

        // GET: SurfBoards/Delete/5
        public async Task<IActionResult> Delete(Guid? id, bool? concurrencyError)
        {
            if (id == null || _context.SurfBoard == null)
            {
                return NotFound();
            }

            var surfBoard = await _context.SurfBoard
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (surfBoard == null)
            {
                if (concurrencyError.GetValueOrDefault())
                {
                    return RedirectToAction(nameof(Index));
                }
                return NotFound();
            }

            if (concurrencyError.GetValueOrDefault())
            {
                ViewData["ConcurrencyErrorMessage"] = "The record you attempted to delete "
                    + "was modified by another user after you got the original values. "
                    + "The delete operation was canceled and the current values in the "
                    + "database have been displayed. If you still want to delete this "
                    + "record, click the Delete button again. Otherwise "
                    + "click the Back to List hyperlink.";
            }
            return View(surfBoard);
        }

        // POST: SurfBoards/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(SurfBoard surfBoard)
        {

            try
            {
                if (await _context.SurfBoard.AnyAsync(m => m.Id == surfBoard.Id))
                {
                    if (surfBoard.ImageName != null)
                    {
                        var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "Images", surfBoard.ImageName);
                        if (System.IO.File.Exists(imagePath))
                            System.IO.File.Delete(imagePath);
                    }
                    _context.SurfBoard.Remove(surfBoard);
                    await _context.SaveChangesAsync();
                }
            return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(Delete), new { concurrencyError = true, id = surfBoard.Id});
            }

        }

        private bool SurfBoardExists(Guid id)
        {
            return _context.SurfBoard.Any(e => e.Id == id);
        }
    }
}
