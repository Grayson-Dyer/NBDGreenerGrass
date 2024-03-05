using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<IActionResult> Details(int? id)
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

        // GET: Bids/Create
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
        public async Task<IActionResult> Create([Bind("ID,Description,ProjectID")] Bid bid)
        {
            if (ModelState.IsValid)
            {
                //Default the stage to unapproved and make the Date Made the current date
                bid.Stage = BidStage.Unapproved;
                bid.DateMade = DateTime.Now;
                
                _context.Add(bid);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Project", new { id = bid.ProjectID });
            }

            return RedirectToAction("Details", "Project", new { id = bid.ProjectID });
        }

        // GET: Bids/Edit/5
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
                return RedirectToAction("Details", "Project", new { id = bid.ProjectID });
            }
            return View(bid);
        }

        // GET: Bids/Delete/5
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
        public async Task<IActionResult> DeleteConfirmed(int id)
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
            return RedirectToAction("Details", "Project", new { id = bid.ProjectID });
        }

        private bool BidExists(int id)
        {
          return _context.Bids.Any(e => e.ID == id);
        }

        
        [ValidateAntiForgeryToken]
        public IActionResult BidReviewed(int id)
        {
            if (_context.Bids == null)
            {
                return Problem("Entity set 'NBDContext.Bids'  is null.");
            }
            var bid = _context.Bids.Find(id);

            if(bid != null)
            {
                bid.BidReviewed();
                _context.Bids.Update(bid);
            }

            _context.SaveChanges();
            return RedirectToAction("Details", "Project", new {id = bid.ProjectID });
        }

        
        [ValidateAntiForgeryToken]
        public  IActionResult BidApproved(int id)
        {
            if (_context.Bids == null)
            {
                return Problem("Entity set 'NBDContext.Bids'  is null.");
            }
            var bid =  _context.Bids.Find(id);

            if (bid != null)
            {
                bid.BidApproved();
                _context.Bids.Update(bid);

                 _context.SaveChanges();

            }

            return RedirectToAction("Details", "Project", new { id = bid.ProjectID });
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
