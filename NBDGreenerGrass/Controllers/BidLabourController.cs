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
                    .Include(b => b.BidMaterials)
                    .FirstOrDefaultAsync(b => b.ID == bidId);

                if (bid == null)
                {
                    return NotFound();
                }

                var projectAmount = await _context.Projects.FirstOrDefaultAsync(p => p.ID == bid.ProjectID);


                var viewModel = new BidLabourViewModel
                {
                    BidID = bidId,
                    ProjectCost = projectAmount.Amount,
                    AvailableLabourTypes = GetAvailableLabourTypes(bidId)
                };

                decimal totalCost = 0;

                foreach (var material in bid.BidMaterials)
                {
                    totalCost += material.InventoryListPrice * material.Quantity;
                }

                foreach (var labour in bid.BidLabours)
                {
                    totalCost += labour.LabourPrice * labour.HoursWorked;
                }

                ViewBag.TotalCost = totalCost;


                return View(viewModel);
            }
            catch (Exception)
            {
                return Problem("An error occurred while retrieving the bid.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateBidLabour(BidLabourViewModel viewModel, Dictionary<int, int> selectedLabourTypes, Dictionary<int, int> hoursWorked)
        {
            if (selectedLabourTypes == null || hoursWorked == null)
            {
                // Handle invalid input
                return View(viewModel);
            }


            var bidLabours = _context.BidLabours.Where(bl => bl.BidID == viewModel.BidID);
            var bidMaterials = _context.BidMaterials.Where(bm => bm.BidID == viewModel.BidID);
            var totalCost = bidLabours.AsEnumerable().Sum(bl => bl.LabourPrice * bl.HoursWorked) + bidMaterials.AsEnumerable().Sum(bm => bm.InventoryListPrice * bm.Quantity);

            // Get the associated Project
            var project = _context.Projects.FirstOrDefault(p => p.ID == _context.Bids.FirstOrDefault(b => b.ID == viewModel.BidID).ProjectID);

            // Check if the total cost exceeds the Project cost
            if (totalCost > project.Amount)
            {
                ModelState.AddModelError("", "The total cost of all BidLabours and BidMaterials cannot exceed the Project cost.");
                viewModel.AvailableLabourTypes = GetAvailableLabourTypes(viewModel.BidID);
                return View(viewModel);
            }

            foreach (var item in selectedLabourTypes)
            {



                int labourID = item.Value;
                int hours = hoursWorked[item.Key];
                var labour = await _context.Labours.FindAsync(labourID);
                totalCost += labour.LabourPrice * hours;

                if(totalCost > project.Amount)
                {
                    ModelState.AddModelError("", "The total cost of all BidLabours and BidMaterials cannot exceed the Project cost.");
                    viewModel.AvailableLabourTypes = GetAvailableLabourTypes(viewModel.BidID);
                    return View(viewModel);
                }
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

                var bid = await _context.Bids.FirstOrDefaultAsync(b => b.ID == viewModel.BidID); 
                bid.DeniedClientReason = null;
                bid.ApprovedClientReason = null;
                bid.DeniedManagerReason = null;
                bid.ApprovedManagerReason = null;
                bid.BidDemote();
                _context.Update(bid);

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
        public async Task<IActionResult> Edit(int? bidId, int? labourId)
        {
            if (bidId == null || labourId == null || _context.BidMaterials == null)
            {
                return NotFound();
            }
            var bid = await _context.Bids
               .Include(b => b.BidMaterials)
               .ThenInclude(bm => bm.Inventory)
               .Include(b => b.BidLabours)
               .FirstOrDefaultAsync(b => b.ID == bidId);

            decimal totalCost = 0;

            foreach (var bm in bid.BidMaterials)
            {
                totalCost += bm.InventoryListPrice * bm.Quantity;
            }

            foreach (var bl in bid.BidLabours)
            {
                totalCost += bl.LabourPrice * bl.HoursWorked;
            }

            ViewBag.ProjectCost = (await _context.Projects.FirstOrDefaultAsync(p => p.ID == bid.ProjectID)).Amount;

            var bidLabour = await _context.BidLabours.FindAsync(bidId, labourId);
            ViewBag.TotalCost = totalCost - ( bidLabour.LabourPrice * bidLabour.HoursWorked );

            if (bidLabour == null)
            {
                return NotFound();
            }
            return View(bidLabour);
        }

        // POST: BidMaterial/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int bidId, int labourId, [Bind("BidID,LabourID,HoursWorked,LabourType,LabourPrice,LabourCost")] BidLabour bidLabour)
        {
            if (bidId != bidLabour.BidID || labourId != bidLabour.LabourID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingBidLabour = await _context.BidLabours.FindAsync(bidId, labourId);
                    if (existingBidLabour == null)
                    {
                        return NotFound();
                    }


                    var bidLabours = _context.BidLabours.Where(bl => bl.BidID == bidLabour.BidID);
                    var bidMaterials = _context.BidMaterials.Where(bm => bm.BidID == bidLabour.BidID);
                    var totalCost = bidLabours.AsEnumerable().Sum(bl => bl.LabourPrice * bl.HoursWorked) + bidMaterials.AsEnumerable().Sum(bm => bm.InventoryListPrice * bm.Quantity);
                    
                    totalCost -= existingBidLabour.LabourPrice * existingBidLabour.HoursWorked;
                    totalCost += bidLabour.LabourPrice * bidLabour.HoursWorked;
                    Bid bid = _context.Bids.FirstOrDefault(b => b.ID == bidLabour.BidID);
                    if (totalCost > _context.Projects.FirstOrDefault(p => p.ID == bid.ProjectID).Amount)
                    {
                        ModelState.AddModelError("", "The total cost of all BidLabours and BidMaterials cannot exceed the Project cost.");
                        var project = await _context.Projects.FirstOrDefaultAsync(p => p.ID == _context.Bids.FirstOrDefault(b => b.ID == bidLabour.BidID).ProjectID);
                        ViewBag.ProjectCost = project.Amount;

                        var bids = await _context.Bids
                          .Include(b => b.BidMaterials)
                          .ThenInclude(bm => bm.Inventory)
                          .Include(b => b.BidLabours)
                          .FirstOrDefaultAsync(b => b.ID == bidId);

                        decimal reloadTotalCost = 0;

                        foreach (var bm in bid.BidMaterials)
                        {
                            reloadTotalCost += bm.InventoryListPrice * bm.Quantity;
                        }

                        foreach (var bl in bid.BidLabours)
                        {
                            reloadTotalCost += bl.LabourPrice * bl.HoursWorked;
                        }

                        ViewBag.TotalCost = reloadTotalCost - (existingBidLabour.HoursWorked * existingBidLabour.LabourPrice);

                        return View(bidLabour);
                    }
                    // Update the quantity of the existing bid material
                    existingBidLabour.HoursWorked = bidLabour.HoursWorked;

                    _context.Update(existingBidLabour);
                    bid.DeniedClientReason = null;
                    bid.ApprovedClientReason = null;
                    bid.DeniedManagerReason = null;
                    bid.ApprovedManagerReason = null;
                    _context.Update(bid);
                    bid.BidDemote();

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
                return RedirectToAction("Details", "Bids", new { id = bidLabour.BidID });

            }
            return View(bidLabour);


        }

        // GET: BidLabour/Delete/5
        public async Task<IActionResult> Delete(int? bidId, int? labourId)
        {
            if (bidId == null || labourId == null || _context.BidLabours == null)
            {
                return NotFound();
            }

            var bidLabour = await _context.BidLabours
                .Include(b => b.Bid)
                .Include(b => b.Labour)
                .FirstOrDefaultAsync(m => m.BidID == bidId && m.LabourID == labourId);
            if (bidLabour == null)
            {
                return NotFound();
            }

            return View(bidLabour);
        }

        // POST: BidLabour/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int bidId, int labourId)
        {
            if (_context.BidLabours == null)
            {
                return Problem("Entity set 'NBDContext.BidLabours' is null.");
            }

            var bidLabour = await _context.BidLabours.FindAsync(bidId, labourId);

            if (bidLabour == null)
            {
                return NotFound();
            }

            int deletedBidId = bidLabour.BidID;

            _context.BidLabours.Remove(bidLabour);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "Bids", new { id = deletedBidId });
        }

        private bool BidLabourExists(int id)
        {
          return _context.BidLabours.Any(e => e.BidID == id);
        }

        private List<LabourItem> GetAvailableLabourTypes(int bidId)
        {

            int[] alreadySelectedItems = _context.BidLabours.Where(bl => bl.BidID == bidId).Select(bl => bl.LabourID).ToArray();

            var avaliableLabourTypes = _context.Labours
                .Include(l => l.BidLabours)
                .Where(l => !alreadySelectedItems.Contains(l.ID))
                .Select(l => new LabourItem
                {
                    ID = l.ID,
                    LabourType = l.LabourType,
                    LabourPrice = l.LabourPrice,
                    LabourCost = l.LabourCost
                }).ToList();

            return avaliableLabourTypes;
        }
    }
}
