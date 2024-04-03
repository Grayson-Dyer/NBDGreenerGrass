using System;
using System.Collections.Generic;
using System.Linq;
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
    public class BidsController : Controller
    {
        private readonly NBDContext _context;

        public BidsController(NBDContext context)
        {
            _context = context;
        }

        // GET: Bids
        public async Task<IActionResult> Index()
        {
            var nBDContext = _context.Bids.Include(b => b.Project);
            return View(await nBDContext.ToListAsync());
        }

        // GET: Bids/Details/5
        [Authorize(Roles = "Management,Designer,Sales")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bid = await _context.Bids
                .Include(b => b.BidMaterials)
                .Include(b => b.BidLabours)
                .FirstOrDefaultAsync(m => m.ID == id);

            var projectHasApprovedBids = _context.Bids.Any(b => b.ProjectID == bid.ProjectID && b.Stage == Enums.BidStage.Approved);
            ViewBag.ProjectHasApprovedBids = projectHasApprovedBids;

            if (bid == null)
            {
                return NotFound();
            }

            decimal totalCost = 0;

            foreach (var material in bid.BidMaterials)
            {
                totalCost += material.InventoryListPrice * material.Quantity;
            }

            foreach (var labour in bid.BidLabours)
            {
                totalCost += labour.LabourPrice * labour.HoursWorked;
            }


            var projectCost = _context.Projects.Find(bid.ProjectID).Amount;


            ViewBag.TotalCost = totalCost;
            ViewBag.ProjectCost = projectCost;
            ViewBag.Profit = projectCost - totalCost;

            return View(bid);
        }


        // GET: Bids/Create
        [Authorize(Roles = "Management,Designer,Sales")]
        public IActionResult Create(int projectId)
        {
            var bid = new Bid { ProjectID = projectId };
            return View();
        }

        // POST: Bids/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Management,Designer,Sales")]
        public async Task<IActionResult> Create([Bind("ID,Description,ProjectID")] Bid bid)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    bid.Stage = Enums.BidStage.Unapproved;


                    _context.Add(bid);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("CreateBidMaterial", "BidMaterial", new { bidID = bid.ID });
                }
                ViewData["ProjectID"] = new SelectList(_context.Projects, "ID", "City", bid.ProjectID);
                ViewData["BidStages"] = new SelectList(Enum.GetValues(typeof(Enums.BidStage)));
                 

            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, contact your system administrator.");
            }

            return View(bid);
        }

        // GET: Bids/Edit/5
        [Authorize(Roles = "Management,Designer,Sales")]
        public async Task<IActionResult> Edit(int? id, int? projectId)
        {
            if (id == null || projectId == null || _context.Bids == null)
            {
                return NotFound();
            }

            var bid = await _context.Bids.FindAsync(id);
            if (bid == null)
            {
                return NotFound();
            }
            ViewData["ProjectID"] = projectId;
            return View(bid);
        }

        // POST: Bids/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Management,Designer,Sales")]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Description,ProjectID")] Bid bid)
        {
            if (id != bid.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Set the stage to unapproved because the bid has been edited
                    bid.Stage = Enums.BidStage.Unapproved;
                    _context.Update(bid);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BidExists(bid.ID))
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
                return RedirectToAction("Details", "Project", new { id = bid.ProjectID });
            }
            return View(bid);
        }

        // GET: Bids/Delete/5
        [Authorize(Roles = "Management")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Bids == null)
            {
                return NotFound();
            }

            var bid = await _context.Bids
                .Include(b => b.Project)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (bid == null)
            {
                return NotFound();
            }

            return View(bid);
        }

        // POST: Bids/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Management")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                if (_context.Bids == null)
                {
                    return Problem("Entity set 'NBDContext.Bids'  is null.");
                }
                var bid = await _context.Bids.FindAsync(id);
                if (bid != null)
                {
                    _context.Bids.Remove(bid);
                }

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, contact your system administrator.");
            }

            return RedirectToAction(nameof(Index));
        }

        private bool BidExists(int id)
        {
          return _context.Bids.Any(e => e.ID == id);
        }


        //GET: Bids/Review/5
   
        [Authorize(Roles = "Management,Designer,Sales")]
        public async Task<IActionResult> Review(int? id)
        {
            if (id == null || _context.Bids == null)
            {
                return NotFound();
            }

            var bid = await _context.Bids
                .FirstOrDefaultAsync(m => m.ID == id);
            if (bid == null)
            {
                return NotFound();
            }

            return View(bid);
        }


        //POST: Bids/Review/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Management,Designer,Sales")]
        public async Task<IActionResult> BidReviewed(int id, [Bind("ID,ProjectID,DeniedManagerReason,ApprovedManagerReason")] Bid bid, string action)
        {
            if (id != bid.ID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var existingBid = await _context.Bids.FindAsync(id);
                    if (existingBid == null)
                    {
                        return NotFound();
                    }
                    if (action == "Approve")
                    {
                        existingBid.BidReviewed();
                        existingBid.ApprovedManagerReason = bid.ApprovedManagerReason;
                    }
                    else if (action == "Deny")
                    {
                        existingBid.DeniedManagerReason = bid.DeniedManagerReason;
                        existingBid.BidDemote();
                    }
                    else
                    {
                        return NotFound();
                    }


                    _context.Update(existingBid);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BidExists(bid.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction("Details", "Bids", new { id = bid.ID });
            }
            return View(bid);
        }


        //GET: Bids/Approve/5
        [Authorize(Roles = "Management")]
        public async Task<IActionResult> Approve(int? id)
        {
            if (id == null || _context.Bids == null)
            {
                return NotFound();
            }

            var bid = await _context.Bids
                .FirstOrDefaultAsync(m => m.ID == id);
            if (bid == null)
            {
                return NotFound();
            }

            return View(bid);
        }

        //POST: Bids/Approve/5
        [HttpPost]
        [Authorize(Roles = "Management")]

        public async Task<IActionResult> BidApproval(int id, [Bind("ID,ProjectID,DeniedClientReason,ApprovedClientReason")] Bid bid, string action)
        {
            if (id != bid.ID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var existingBid = await _context.Bids.FindAsync(id);
                    if (existingBid == null)
                    {
                        return NotFound();
                    }
                    if (action == "Approve")
                    {
                        existingBid.BidApproved();
                        existingBid.ApprovedClientReason = bid.ApprovedClientReason;
                    }
                    else if (action == "Deny")
                    {
                        existingBid.DeniedClientReason = bid.DeniedClientReason;
                        existingBid.BidDemote();
                    }
                    else
                    {
                        return NotFound();
                    }


                    _context.Update(existingBid);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BidExists(bid.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction("Details", "Bids", new { id = bid.ID });
            }
            return View(bid);
        }

        [ValidateAntiForgeryToken]
        public IActionResult BidDemote(int id)
        {
            if (_context.Bids == null)
            {
                return Problem("Entity set 'NBDContext.Bids'  is null.");
            }
            var bid = _context.Bids.Find(id);

            if (bid != null)
            {
                bid.BidDemote();
                _context.Bids.Update(bid);
                _context.SaveChanges();

            }

            return RedirectToAction("Details", "Project", new { id = bid.ProjectID });
        }

    }

}
