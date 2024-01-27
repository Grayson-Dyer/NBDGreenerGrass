using System.ComponentModel.DataAnnotations;

namespace NBDGreenerGrass.Models
{
    public class BidMaterial
    { 

        [Required(ErrorMessage = "Inventory ID is required")]
        public int InventoryID { get; set; }
        public Inventory inventory { get; set; }

        [Required(ErrorMessage = "Bid ID is required")]
        public int BidID { get; set; }
        public Bid Bid { get; set; }
    }
}
