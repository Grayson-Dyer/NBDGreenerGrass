using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NBDGreenerGrass.Data;
using NBDGreenerGrass.Enums;
using NBDGreenerGrass.Models;

namespace NBDGreenerGrass.Controllers
{
    [AllowAnonymous]
    public class ProjectController : Controller
    {
        private readonly NBDContext _context;

        public ProjectController(NBDContext context)
        {
            _context = context;
        }

        // GET: Project
        public async Task<IActionResult> Index(string SearchString, string StreetString, int? page, string actionButton, string sortDirection = "asc", string sortField = "Client")
        {
            var nBDContext = _context.Projects
                .Include(c => c.Client)
                .AsQueryable(); // Convert to IQueryable for dynamic querying

            ViewData["Filtering"] = "btn-outline-secondary";
            int numberFilters = 0;

            if (!string.IsNullOrEmpty(SearchString))
            {
                nBDContext = nBDContext.Where(p => p.Client.Name.ToUpper().Contains(SearchString.ToUpper()));
                numberFilters++;
            }
            if (!string.IsNullOrEmpty(StreetString))
            {
                nBDContext = nBDContext.Where(p => p.Street.ToUpper().Contains(StreetString.ToUpper()));
                numberFilters++;
            }

            if (numberFilters != 0)
            {
                ViewData["Filtering"] = " btn-danger";
                ViewData["numberFilters"] = "(" + numberFilters.ToString() + " Filter" + (numberFilters > 1 ? "s" : "") + " Applied)";
            }

            if (!string.IsNullOrEmpty(actionButton))
            {
                page = 1;

                // Update sort direction
                sortDirection = sortDirection == "asc" ? "desc" : "asc";

                // Update sort field
                sortField = actionButton;
            }

            // Apply sorting based on sortField and sortDirection
            switch (sortField)
            {
                case "Client":
                    nBDContext = sortDirection == "asc" ? nBDContext.OrderBy(p => p.Client.Name) : nBDContext.OrderByDescending(p => p.Client.Name);
                    break;
                case "Start":
                    nBDContext = sortDirection == "asc" ? nBDContext.OrderBy(p => p.Start) : nBDContext.OrderByDescending(p => p.Start);
                    break;
                // Add cases for other fields if needed
                default:
                    nBDContext = nBDContext.OrderBy(p => p.Client.Name); // Default sorting
                    break;
            }

            // Set sort for next time
            ViewData["sortField"] = sortField;
            ViewData["sortDirection"] = sortDirection;

            return View(await nBDContext.ToListAsync());
        }

        // GET: Project/Details/5
        [HttpGet]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Management,Designer,Sales")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }



            var project = await _context.Projects
                .Include(p => p.Client)
                .Include(p => p.Bids)
                .FirstOrDefaultAsync(m => m.ID == id);


            if (project == null)
            {
                return NotFound();
            }

            bool anyBidApproved = project.Bids.Any(b => b.Stage == BidStage.Approved);

            ViewBag.AnyBidApproved = anyBidApproved;


            return View(project);
        }

        // GET: Project/Create
        [HttpGet]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Management,Designer,Sales")]
        public IActionResult Create()
        {
            ViewData["ClientID"] = new SelectList(_context.Clients, "ID", "Name");
            return View();
        }

        // POST: Project/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Management,Designer,Sales")]
        public async Task<IActionResult> Create([Bind("ID,Start,End,Amount,Created,Street,City,Province,Postal,Desc,ClientID")] Project project)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(project);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ViewData["ClientID"] = new SelectList(_context.Clients, "ID", "Name", project.ClientID);
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, contact your system administrator.");
            }
            
            return View(project);
        }

        // GET: Project/Edit/5
        [HttpGet]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Management,Designer,Sales")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            ViewData["ClientID"] = new SelectList(_context.Clients, "ID", "Name", project.ClientID);
            return View(project);
        }

        // POST: Project/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Management,Designer,Sales")]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Start,End,Amount,Created,Street,City,Province,Postal,Desc,ClientID")] Project project)
        {
            if (id != project.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(project);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, contact your system administrator.");
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientID"] = new SelectList(_context.Clients, "ID", "Name", project.ClientID);
            return View(project);
        }

        // GET: Project/Delete/5
        [HttpGet]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Management")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(p => p.Client)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: Project/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Management")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            try
            {
                if (_context.Projects == null)
                {
                    return Problem("Entity set 'NBDContext.Projects'  is null.");
                }
                var project = await _context.Projects.FindAsync(id);
                if (project != null)
                {
                    _context.Projects.Remove(project);
                }

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, contact your system administrator.");
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ProjectExists(int id)
        {
          return _context.Projects.Any(e => e.ID == id);
        }
    }
}
