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
    public class BidMaterialController : Controller
    {
        private readonly NBDContext _context;

        public BidMaterialController(NBDContext context)
        {
            _context = context;
        }

        // GET: BidMaterial
        /*public async Task<IActionResult> Index()
        {
            var nBDContext = _context.BidMaterials.Include(b => b.Bid).Include(b => b.Inventory);
            return View(await nBDContext.ToListAsync());
        }

        // GET: BidMaterial/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BidMaterials == null)
            {
                return NotFound();
            }

            var bidMaterial = await _context.BidMaterials
                .Include(b => b.Bid)
                .Include(b => b.Inventory)
                .FirstOrDefaultAsync(m => m.BidID == id);
            if (bidMaterial == null)
            {
                return NotFound();
            }

            return View(bidMaterial);
        }*/

        // GET: BidMaterial/CreateBidMaterial
        public async Task<IActionResult> CreateBidMaterial(int bidId)
        {
            try
            {
                var bid = await _context.Bids
                    .Include(b => b.BidMaterials)
                    .ThenInclude(bm => bm.Inventory)
                    .FirstOrDefaultAsync(b => b.ID == bidId);

                if (bid == null)
                {
                    return NotFound();
                }

                var viewModel = new BidMaterialViewModel
                {
                    BidID = bidId,
                    AvailableInventory = _context.Inventories
                        .Include(i => i.BidMaterials) // Include BidMaterials to avoid null reference
                        .Select(i => new InventoryItem
                        {
                            ID = i.ID,
                            InventoryDesc = i.InventoryDesc,
                            InventorySize = i.InventorySize,
                            InventoryCode = i.InventoryCode,
                            InventoryListPrice = i.InventoryListPrice
                        })
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                // Log the exception or display it in the view
                Console.WriteLine(ex.ToString());
                return Content(ex.ToString());
            }
        }

        // POST: BidMaterial/CreateBidMaterial
        [HttpPost]
        public async  Task<IActionResult> CreateBidMaterial(BidMaterialViewModel viewModel, int[] selectedInventory, int[] quantities)
        {
            if (selectedInventory == null || quantities == null)
            {
                // Handle invalid input
                return View(viewModel);
            }


            for (int i = 0; i < selectedInventory.Length; i++)
            {
                int inventoryID = selectedInventory[i];
                int quantity = quantities[i];
                var inventory = await _context.Inventories.FindAsync(inventoryID);
                // Create a new BidMaterial instance and save it to the database
                var bidMaterial = new BidMaterial
                {
                    BidID = viewModel.BidID,
                    InventoryID = inventoryID,
                    Quantity = quantity,
                    InventoryDesc = inventory.InventoryDesc,
                    InventorySize = inventory.InventorySize,
                    InventoryCode = inventory.InventoryCode,
                    InventoryListPrice = inventory.InventoryListPrice
                };

                await _context.BidMaterials.AddAsync(bidMaterial);
            }

            await _context.SaveChangesAsync();

            // Redirect or return a view as needed
            return RedirectToAction("Details", "Bids", new { id = viewModel.BidID });
        }





        // GET: BidMaterial/Create 
        /* public IActionResult Create(int bidId)
         {
             var bidMaterial = new BidMaterial { BidID = bidId };
             return View();
         }

         // POST: BidMaterial/Create
         // To protect from overposting attacks, enable the specific properties you want to bind to.
         // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
         [HttpPost]
         [ValidateAntiForgeryToken]
         public async Task<IActionResult> Create([Bind("ID,BidID,InventoryID,Quantity,InventoryDesc,InventorySize,InventoryCode,InventoryListPrice")] BidMaterial bidMaterial)
         {
             if (ModelState.IsValid)
             {
                 _context.Add(bidMaterial);
                 await _context.SaveChangesAsync();
                 return RedirectToAction(nameof(Index));
             }
             ViewData["BidID"] = new SelectList(_context.Bids, "ID", "ID", bidMaterial.BidID);
             ViewData["InventoryID"] = new SelectList(_context.Inventories, "ID", "InventoryCode", bidMaterial.InventoryID);
             return View(bidMaterial);
         }*/

        // GET: BidMaterial/Edit/5
        public async Task<IActionResult> Edit(int? bidId, int? inventoryId)
        {
            if (bidId == null || inventoryId == null || _context.BidMaterials == null)
            {
                return NotFound();
            }

            var bidMaterial = await _context.BidMaterials.FindAsync(bidId, inventoryId);
            if (bidMaterial == null)
            {
                return NotFound();
            }
            return View(bidMaterial);
        }

        // POST: BidMaterial/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int bidId, int inventoryId, [Bind("BidID,InventoryID,Quantity,InventoryDesc,InventorySize,InventoryCode,InventoryListPrice")] BidMaterial bidMaterial)
        {
            if (bidId != bidMaterial.BidID || inventoryId != bidMaterial.InventoryID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingBidMaterial = await _context.BidMaterials.FindAsync(bidId, inventoryId);
                    if (existingBidMaterial == null)
                    {
                        return NotFound();
                    }

                    // Update the quantity of the existing bid material
                    existingBidMaterial.Quantity = bidMaterial.Quantity;

                    _context.Update(existingBidMaterial);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BidMaterialExists(bidMaterial.BidID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "Bids", new { id = bidMaterial.BidID });

            }
            return View(bidMaterial);


        }

        // GET: BidMaterial/Delete/5
        /*public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BidMaterials == null)
            {
                return NotFound();
            }

            var bidMaterial = await _context.BidMaterials
                .Include(b => b.Bid)
                .Include(b => b.Inventory)
                .FirstOrDefaultAsync(m => m.BidID == id);
            if (bidMaterial == null)
            {
                return NotFound();
            }

            return View(bidMaterial);
        }

        // POST: BidMaterial/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BidMaterials == null)
            {
                return Problem("Entity set 'NBDContext.BidMaterials'  is null.");
            }
            var bidMaterial = await _context.BidMaterials.FindAsync(id);
            if (bidMaterial != null)
            {
                _context.BidMaterials.Remove(bidMaterial);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        */
        private bool BidMaterialExists(int id)
        {
          return _context.BidMaterials.Any(e => e.BidID == id);
        }
        
    }
}
