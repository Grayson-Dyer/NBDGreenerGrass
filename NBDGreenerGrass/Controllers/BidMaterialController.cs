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
        [HttpGet]
        [Authorize(Roles = "Management,Designer,Sales")]
        public async Task<IActionResult> CreateBidMaterial(int bidId, string returnUrl = null, string searchInventoryDesc = null, string searchInventoryCode = null, string sortOrder = null)

        {
            try
            {
                
                var bid = await _context.Bids
                    .Include(b => b.BidMaterials)
                    .ThenInclude(bm => bm.Inventory)
                    .Include(b => b.BidLabours)
                    .FirstOrDefaultAsync(b => b.ID == bidId);

                if (bid == null)
                {
                    return NotFound();
                }

                // Create a new BidMaterialViewModel instance and populate it with the necessary data
                var viewModel = new BidMaterialViewModel
                {
                    ProjectCost = _context.Projects.FirstOrDefault(p => p.ID == bid.ProjectID).Amount,
                    BidID = bidId,
                    AvailableInventory = GetAvailableInventory(bidId),
                    ReturnUrl = returnUrl,
                    SearchInventoryDesc = searchInventoryDesc,
                    SearchInventoryCode = searchInventoryCode
                };
                
                viewModel.CurrentInventoryDescFilter = searchInventoryDesc;
                viewModel.CurrentInventoryCodeFilter = searchInventoryCode;

                // Filter the available inventory based on the searchInventoryDesc
                
                if (!string.IsNullOrEmpty(searchInventoryDesc) || !string.IsNullOrEmpty(searchInventoryCode))
                {

                    if (!string.IsNullOrEmpty(searchInventoryDesc) && !string.IsNullOrEmpty(searchInventoryCode))
                    {
                        string searchDesc = searchInventoryDesc.ToLower();
                        string searchCode = searchInventoryCode.ToLower();

                        viewModel.AvailableInventory = viewModel.AvailableInventory
                            .Where(i => i.InventoryDesc.ToLower().Contains(searchDesc) && i.InventoryCode.ToLower().Contains(searchCode))
                            .ToList();
                    }
                    else if (!string.IsNullOrEmpty(searchInventoryDesc))
                    {
                        string searchDesc = searchInventoryDesc.ToLower();

                        viewModel.AvailableInventory = viewModel.AvailableInventory
                            .Where(i => i.InventoryDesc.ToLower().Contains(searchDesc))
                            .ToList();
                    }
                    else if (!string.IsNullOrEmpty(searchInventoryCode))
                    {
                        string searchCode = searchInventoryCode.ToLower();

                        viewModel.AvailableInventory = viewModel.AvailableInventory
                            .Where(i => i.InventoryCode.ToLower().Contains(searchCode))
                            .ToList();
                    }
                }

                



                // Calculate the total cost of all BidMaterials and BidLabours
                decimal totalCost = 0;

                foreach (var bidMaterial in bid.BidMaterials)
                {
                    totalCost += bidMaterial.InventoryListPrice * bidMaterial.Quantity;
                }

                foreach (var bidLabour in bid.BidLabours)
                {
                    totalCost += bidLabour.LabourPrice * bidLabour.HoursWorked;
                }




                ViewBag.TotalCost = totalCost;

                viewModel.InventoryDescSort = string.IsNullOrEmpty(sortOrder) ? "inventory_desc" : "";
                viewModel.InventoryCodeSort = sortOrder == "inventory_code" ? "inventory_code_desc" : "inventory_code";
                viewModel.InventorySizeSort = sortOrder == "inventory_size" ? "inventory_size_desc" : "inventory_size";
                viewModel.InventoryListPriceSort = sortOrder == "inventory_list_price" ? "inventory_list_price_desc" : "inventory_list_price";

                viewModel.SortOrder = sortOrder;

                switch (sortOrder)
                {
                    case "inventory_desc":
                        viewModel.AvailableInventory = viewModel.AvailableInventory.OrderByDescending(i => i.InventoryDesc).ToList();
                        break;
                    case "inventory_code":
                        viewModel.AvailableInventory = viewModel.AvailableInventory.OrderBy(i => i.InventoryCode).ToList();
                        break;
                    case "inventory_code_desc":
                        viewModel.AvailableInventory = viewModel.AvailableInventory.OrderByDescending(i => i.InventoryCode).ToList();
                        break;
                    case "inventory_size":
                        viewModel.AvailableInventory = viewModel.AvailableInventory.OrderBy(i => i.InventorySize).ToList();
                        break;
                    case "inventory_size_desc":
                        viewModel.AvailableInventory = viewModel.AvailableInventory.OrderByDescending(i => i.InventorySize).ToList();
                        break;
                    case "inventory_list_price":
                        viewModel.AvailableInventory = viewModel.AvailableInventory.OrderBy(i => i.InventoryListPrice).ToList();
                        break;
                    case "inventory_list_price_desc":
                        viewModel.AvailableInventory = viewModel.AvailableInventory.OrderByDescending(i => i.InventoryListPrice).ToList();
                        break;
                    default:
                        viewModel.AvailableInventory = viewModel.AvailableInventory.OrderBy(i => i.InventoryDesc).ToList();
                        break;

                }

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
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Management,Designer")]
        public async Task<IActionResult> CreateBidMaterial(BidMaterialViewModel viewModel, Dictionary<int, int> selectedInventory, Dictionary<int, int> quantities)
        {

            if (selectedInventory == null || quantities == null)
            {
                // Handle invalid input
                return View(viewModel);
            }

            var bidLabours = _context.BidLabours.Where(bl => bl.BidID == viewModel.BidID);
            var bidMaterials = _context.BidMaterials.Where(bm => bm.BidID == viewModel.BidID);
            var totalCost = bidLabours.AsEnumerable().Sum(bl => bl.LabourPrice * bl.HoursWorked) + bidMaterials.AsEnumerable().Sum(bm => bm.InventoryListPrice * bm.Quantity);

            var project = _context.Projects.FirstOrDefault(p => p.ID == _context.Bids.FirstOrDefault(b => b.ID == viewModel.BidID).ProjectID);

            if (totalCost > project.Amount)
            {
                ModelState.AddModelError("", "The total cost of all BidLabours and BidMaterials cannot exceed the Project cost.");
                viewModel.AvailableInventory = GetAvailableInventory(viewModel.BidID);
                return View(viewModel);
            }

            foreach (var item in selectedInventory)
            {
                int inventoryID = item.Value;
                int quantity = quantities[item.Key];
                var inventory = await _context.Inventories.FindAsync(inventoryID);
                totalCost += inventory.InventoryListPrice * quantity;


                if (totalCost > project.Amount)
                {
                    ModelState.AddModelError("", "The total cost of all BidLabours and BidMaterials cannot exceed the Project cost.");
                    viewModel.AvailableInventory = GetAvailableInventory(viewModel.BidID);
                    return View(viewModel);
                }
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
                var bid = await _context.Bids.FirstOrDefaultAsync(b => b.ID == viewModel.BidID);
                bid.DeniedClientReason = null;
                bid.ApprovedClientReason = null;
                bid.DeniedManagerReason = null;
                bid.ApprovedManagerReason = null;
                bid.BidDemote();
                _context.Update(bid);

                await _context.BidMaterials.AddAsync(bidMaterial);
                
            }


            await _context.SaveChangesAsync();

            // Redirect or return a view as needed
            if (!string.IsNullOrEmpty(viewModel.ReturnUrl))
            {
                return Redirect(viewModel.ReturnUrl);
            }
            else
            {
                return RedirectToAction("CreateBidLabour", "BidLabour", new { bidID = viewModel.BidID });
            }
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
        [Authorize(Roles = "Management,Designer")]
        public async Task<IActionResult> Edit(int? bidId, int? inventoryId)
        {
            if (bidId == null || inventoryId == null || _context.BidMaterials == null)
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


            var bidMaterial = await _context.BidMaterials.FindAsync(bidId, inventoryId);

            ViewBag.TotalCost = totalCost - (bidMaterial.Quantity * bidMaterial.InventoryListPrice);

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
        [Authorize(Roles = "Management,Designer")]

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


                    var bidLabours = _context.BidLabours.Where(bl => bl.BidID == bidMaterial.BidID);
                    var bidMaterials = _context.BidMaterials.Where(bm => bm.BidID == bidMaterial.BidID);
                    var totalCost = bidLabours.AsEnumerable().Sum(bl => bl.LabourPrice * bl.HoursWorked) + bidMaterials.AsEnumerable().Sum(bm => bm.InventoryListPrice * bm.Quantity);
                    Bid bid = _context.Bids.FirstOrDefault(b => b.ID == bidMaterial.BidID);

                    totalCost -= existingBidMaterial.InventoryListPrice * existingBidMaterial.Quantity;
                    totalCost += bidMaterial.InventoryListPrice * bidMaterial.Quantity;
                    if (totalCost > _context.Projects.FirstOrDefault(p => p.ID == bid.ProjectID).Amount)
                    {
                        ModelState.AddModelError("", "The total cost of all BidLabours and BidMaterials cannot exceed the Project cost.");
                        var project = await _context.Projects.FirstOrDefaultAsync(p => p.ID == _context.Bids.FirstOrDefault(b => b.ID == bidMaterial.BidID).ProjectID);
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

                        ViewBag.TotalCost = reloadTotalCost - (existingBidMaterial.Quantity * existingBidMaterial.InventoryListPrice);


                        return View(bidMaterial);
                    }

                    // Update the quantity of the existing bid material
                    existingBidMaterial.Quantity = bidMaterial.Quantity;

                    _context.Update(existingBidMaterial);
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
        [Authorize(Roles = "Management")]
        public async Task<IActionResult> Delete(int? bidId, int? inventoryId)
        {
            if (bidId == null || inventoryId == null || _context.BidMaterials == null)
            {
                return NotFound();
            }

            var bidMaterial = await _context.BidMaterials
                .Include(b => b.Bid)
                .Include(b => b.Inventory)
                .FirstOrDefaultAsync(m => m.BidID == bidId && m.InventoryID == inventoryId);
            if (bidMaterial == null)
            {
                return NotFound();
            }

            return View(bidMaterial);
        }

        // POST: BidMaterial/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Management")]

        public async Task<IActionResult> DeleteConfirmed(int bidId, int inventoryId)
        {
            if (_context.BidMaterials == null)
            {
                return Problem("Entity set 'NBDContext.BidMaterials'  is null.");
            }
            var bidMaterial = await _context.BidMaterials.FindAsync(bidId, inventoryId);
            if (bidMaterial != null)
            {
                _context.BidMaterials.Remove(bidMaterial);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Bids", new { id = bidId });
        }

        private bool BidMaterialExists(int id)
        {
          return _context.BidMaterials.Any(e => e.BidID == id);
        }

        public List<InventoryItem> GetAvailableInventory(int bidId)
        {
            int[] alreadySelectedItems = _context.BidMaterials.Where(bm => bm.BidID == bidId).Select(bl => bl.InventoryID).ToArray();

            return _context.Inventories
                .Include(i => i.BidMaterials) // Include BidMaterials to avoid null reference
                .Where(i => !alreadySelectedItems.Contains(i.ID))
                .Select(i => new InventoryItem
                {
                    ID = i.ID,
                    InventoryDesc = i.InventoryDesc,
                    InventorySize = i.InventorySize,
                    InventoryCode = i.InventoryCode,
                    InventoryListPrice = i.InventoryListPrice
                }).ToList();
        }
        
        
        
    }
}
