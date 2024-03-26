using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NBDGreenerGrass.Data;
using NBDGreenerGrass.Models;

namespace NBDGreenerGrass.Controllers
{
    public class BidLabourController : Controller
    {
        private readonly NBDContext _context;

        public BidLabourController(NBDContext context)
        {
            _context = context;
        }

        //Get
        public async Task<IActionResult> CreateBidLabour(int bidId)
        {
            try
            {
                var bid = await _context.Bids
                    .Include(b => b.BidLabours)
                    .ThenInclude(bl => bl.Labour)
                    .FirstOrDefaultAsync(b => b.ID == bidId);

                if (bid == null)
                {
                    return NotFound();
                }



                var viewModel = new BidLabourViewModel
                {
                    BidID = bidId,
                    AvailableLabourTypes = _context.Labours
                    .Include(l => l.BidLabours)
                    .Select(l => new LabourItem
                    {
                        ID = l.ID,
                        LabourType = l.LabourType,
                        LabourPrice = l.LabourPrice,
                        LabourCost = l.LabourCost
                    }).ToList()
                };

                return View(viewModel);
            }
            catch (Exception)
            {
                return Problem("An error occurred while retrieving the bid.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateBidLabour(BidLabourViewModel viewModel, int[] selectedLabourTypes, int[] hoursWorked)
        {
            if (selectedLabourTypes == null || hoursWorked == null)
            {
                // Handle invalid input
                return View(viewModel);
            }


            for (int i = 0; i < selectedLabourTypes.Length; i++)
            {
                int labourID = selectedLabourTypes[i];
                int hours = hoursWorked[i];
                var labour = await _context.Labours.FindAsync(labourID);
                // Create a new bidLabour instance and save it to the database
                var bidLabour = new BidLabour
                {
                    BidID = viewModel.BidID,
                    LabourID = labourID,
                    HoursWorked = hours,
                    LabourType = labour.LabourType,
                    LabourPrice = labour.LabourPrice,
                    LabourCost = labour.LabourCost
                };

                await _context.BidLabours.AddAsync(bidLabour);
            }

            await _context.SaveChangesAsync();

            // Redirect or return a view as needed
            return RedirectToAction("Details", "Bids", new { id = viewModel.BidID });
        }


        /*// GET: BidLabour
        public async Task<IActionResult> Index()
        {
            var nBDContext = _context.BidLabours.Include(b => b.Bid).Include(b => b.Labour);
            return View(await nBDContext.ToListAsync());
        }

        // GET: BidLabour/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BidLabours == null)
            {
                return NotFound();
            }

            var bidLabour = await _context.BidLabours
                .Include(b => b.Bid)
                .Include(b => b.Labour)
                .FirstOrDefaultAsync(m => m.BidID == id);
            if (bidLabour == null)
            {
                return NotFound();
            }

            return View(bidLabour);
        }*/

        /*// GET: BidLabour/Create
        public IActionResult Create()
        {
            ViewData["BidID"] = new SelectList(_context.Bids, "ID", "ID");
            ViewData["LabourID"] = new SelectList(_context.Set<Labour>(), "ID", "LabourType");
            return View();
        }

        // POST: BidLabour/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LabourID,BidID,HoursWorked,LabourType,LabourPrice,LabourCost")] BidLabour bidLabour)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bidLabour);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BidID"] = new SelectList(_context.Bids, "ID", "ID", bidLabour.BidID);
            ViewData["LabourID"] = new SelectList(_context.Set<Labour>(), "ID", "LabourType", bidLabour.LabourID);
            return View(bidLabour);
        }*/

        // GET: BidLabour/Edit/5
        /* public async Task<IActionResult> Edit(int? id)
         {
             if (id == null || _context.BidLabours == null)
             {
                 return NotFound();
             }

             var bidLabour = await _context.BidLabours.FindAsync(id);
             if (bidLabour == null)
             {
                 return NotFound();
             }
             ViewData["BidID"] = new SelectList(_context.Bids, "ID", "ID", bidLabour.BidID);
             ViewData["LabourID"] = new SelectList(_context.Set<Labour>(), "ID", "LabourType", bidLabour.LabourID);
             return View(bidLabour);
         }

         // POST: BidLabour/Edit/5
         // To protect from overposting attacks, enable the specific properties you want to bind to.
         // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
         [HttpPost]
         [ValidateAntiForgeryToken]
         public async Task<IActionResult> Edit(int id, [Bind("LabourID,BidID,HoursWorked,LabourType,LabourPrice,LabourCost")] BidLabour bidLabour)
         {
             if (id != bidLabour.BidID)
             {
                 return NotFound();
             }

             if (ModelState.IsValid)
             {
                 try
                 {
                     _context.Update(bidLabour);
                     await _context.SaveChangesAsync();
                 }
                 catch (DbUpdateConcurrencyException)
                 {
                     if (!BidLabourExists(bidLabour.BidID))
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
             ViewData["BidID"] = new SelectList(_context.Bids, "ID", "ID", bidLabour.BidID);
             ViewData["LabourID"] = new SelectList(_context.Set<Labour>(), "ID", "LabourType", bidLabour.LabourID);
             return View(bidLabour);
         }*/

        // GET: BidLabour/Delete/5
        /* public async Task<IActionResult> Delete(int? id)
         {
             if (id == null || _context.BidLabours == null)
             {
                 return NotFound();
             }

             var bidLabour = await _context.BidLabours
                 .Include(b => b.Bid)
                 .Include(b => b.Labour)
                 .FirstOrDefaultAsync(m => m.BidID == id);
             if (bidLabour == null)
             {
                 return NotFound();
             }

             return View(bidLabour);
         }

         // POST: BidLabour/Delete/5
         [HttpPost, ActionName("Delete")]
         [ValidateAntiForgeryToken]
         public async Task<IActionResult> DeleteConfirmed(int id)
         {
             if (_context.BidLabours == null)
             {
                 return Problem("Entity set 'NBDContext.BidLabours'  is null.");
             }
             var bidLabour = await _context.BidLabours.FindAsync(id);
             if (bidLabour != null)
             {
                 _context.BidLabours.Remove(bidLabour);
             }

             await _context.SaveChangesAsync();
             return RedirectToAction(nameof(Index));
         }*/

        private bool BidLabourExists(int id)
        {
          return _context.BidLabours.Any(e => e.BidID == id);
        }
    }
}
