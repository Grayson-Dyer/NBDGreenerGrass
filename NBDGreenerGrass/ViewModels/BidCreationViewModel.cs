using NBDGreenerGrass.Models;
using System.Collections.Generic;

namespace NBDGreenerGrass.ViewModels
{
    public class BidCreationViewModel
    {
        public Bid Bid { get; set; }
        public List<Labour> Labours { get; set; }
        public List<Inventory> Inventories { get; set; }
        public decimal ProjectCost { get; set; }

        // Properties to hold the selected labour IDs and inventory IDs
        public List<int> SelectedLabourIds { get; set; }
        public List<int> SelectedInventoryIds { get; set; }

        public BidCreationViewModel()
        {
            SelectedLabourIds = new List<int>();
            SelectedInventoryIds = new List<int>();
        }
    }
}
