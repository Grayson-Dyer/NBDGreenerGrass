using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NBDGreenerGrass.Data;
using NBDGreenerGrass.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NBDGreenerGrass.Controllers
{
    [AllowAnonymous]
    public class ClientsController : Controller
    {
        private readonly NBDContext _context;

        public ClientsController(NBDContext context)
        {
            _context = context;
        }

        // GET: Clients
        public async Task<IActionResult> Index(string SearchString, int? ClientID, string? ContactFirstString, string? ContactLastString,
      int? page, string actionButton, string sortDirection = "asc", string sortField = "Client")
        {
            var nBDContext = _context.Clients
                .Include(c => c.ClientRole)
                .Include(c => c.Projects)
                .AsNoTracking();

            //Count the number of filters applied - start by assuming no filters
            ViewData["Filtering"] = "btn-outline-secondary";
            int numberFilters = 0;
            //Then in each "test" for filtering, add to the count of Filters applied

            //List of sort options.
            //NOTE: make sure this array has matching values to the column headings
            string[] sortOptions = new[] { "Name", "ContactFirst", "ContactLast", "Phone", "ClientRole" };

            //Add as many filters as needed
            if (ClientID.HasValue)
            {
                nBDContext = nBDContext.Where(p => p.ID == ClientID);
                numberFilters++;
            }

            if (!string.IsNullOrEmpty(SearchString))
            {
                nBDContext = nBDContext.Where(p => p.Name.ToUpper().Contains(SearchString.ToUpper()));
                numberFilters++;
            }

            if (!string.IsNullOrEmpty(ContactLastString))
            {
                nBDContext = nBDContext.Where(p => p.ContactLast.ToUpper().Contains(ContactLastString.ToUpper()));
                numberFilters++;
            }

            if (!string.IsNullOrEmpty(ContactFirstString))
            {
                nBDContext = nBDContext.Where(p => p.ContactFirst.ToUpper().Contains(ContactFirstString.ToUpper()));
                numberFilters++;
            }
            //Give feedback about the state of the filters
            if (numberFilters != 0)
            {
                //Toggle the Open/Closed state of the collapse depending on if we are filtering
                ViewData["Filtering"] = "btn-danger";
                //Show how many filters have been applied
                ViewData["numberFilters"] = "(" + numberFilters.ToString()
                    + " Filter" + (numberFilters > 1 ? "s" : "") + " Applied)";
                //Keep the Bootstrap collapse open
                //@ViewData["ShowFilter"] = " show";
            }

            //Before we sort, see if we have called for a change of filtering or sorting
            if (!string.IsNullOrEmpty(actionButton)) //Form Submitted!
            {
                page = 1;//Reset page to start

                if (sortOptions.Contains(actionButton))//Change of sort is requested
                {
                    if (actionButton == sortField) //Reverse order on same field
                    {
                        sortDirection = sortDirection == "asc" ? "desc" : "asc";
                    }
                    sortField = actionButton;//Sort by the button clicked
                }
            }

            //Now we know which field and direction to sort by
            if (sortField == "Name")
            {
                if (sortDirection == "asc")
                {
                    nBDContext = nBDContext.OrderBy(p => p.Name);
                }
                else
                {
                    nBDContext = nBDContext.OrderByDescending(p => p.Name);
                }
            }
            else if (sortField == "ContactFirst")
            {
                if (sortDirection == "asc")
                {
                    nBDContext = nBDContext.OrderBy(p => p.ContactFirst);
                }
                else
                {
                    nBDContext = nBDContext.OrderByDescending(p => p.ContactFirst);
                }
            }
            else if (sortField == "ContactLast")
            {
                if (sortDirection == "asc")
                {
                    nBDContext = nBDContext.OrderBy(p => p.ContactLast);
                }
                else
                {
                    nBDContext = nBDContext.OrderByDescending(p => p.ContactLast);
                }
            }
            else if (sortField == "Phone")
            {
                if (sortDirection == "asc")
                {
                    nBDContext = nBDContext.OrderBy(p => p.Phone);
                }
                else
                {
                    nBDContext = nBDContext.OrderByDescending(p => p.Phone);
                }
            }
            else if (sortField == "ClientRole")
            {
                if (sortDirection == "asc")
                {
                    nBDContext = nBDContext.OrderBy(p => p.ClientRole.Role);
                }
                else
                {
                    nBDContext = nBDContext.OrderByDescending(p => p.ClientRole.Role);
                }
            }

            //Set sort for next time
            ViewData["sortField"] = sortField;
            ViewData["sortDirection"] = sortDirection;

            return View(await nBDContext.ToListAsync());
        }


        // GET: Clients/Details/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Management,Designer,Sales")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Clients == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .Include(c => c.ClientRole)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // GET: Clients/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Management,Designer,Sales")]
        public IActionResult Create()
        {
            ViewData["ClientRoleID"] = new SelectList(_context.ClientRoles, "ID", "Role");
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Management,Designer,Sales")]
        public async Task<IActionResult> Create([Bind("ID,Name,ContactFirst,ContactLast,Phone,Street,City,Postal,Province,ClientRoleID")] Client client)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(client);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ViewData["ClientRoleID"] = new SelectList(_context.ClientRoles, "ID", "Role", client.ClientRoleID);
            }
            catch
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, contact your system administrator.");
            }

            return View(client);
        }

        // GET: Clients/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Management,Designer,Sales")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Clients == null)
            {
                return NotFound();
            }

            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            ViewData["ClientRoleID"] = new SelectList(_context.ClientRoles, "ID", "Role", client.ClientRoleID);
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Management,Designer,Sales")]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,ContactFirst,ContactLast,Phone,Street,City,Postal,Province,ClientRoleID")] Client client)
        {
            if (id != client.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.ID))
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
            ViewData["ClientRoleID"] = new SelectList(_context.ClientRoles, "ID", "Role", client.ClientRoleID);
            return View(client);
        }

        // GET: Clients/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Management")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Clients == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .Include(c => c.ClientRole)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Management")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                if (_context.Clients == null)
                {
                    return Problem("Entity set 'NBDContext.Clients'  is null.");
                }
                var client = await _context.Clients.FindAsync(id);
                if (client != null)
                {
                    _context.Clients.Remove(client);
                }

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, contact your system administrator.");
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(int id)
        {
          return _context.Clients.Any(e => e.ID == id);
        }
    }
}
