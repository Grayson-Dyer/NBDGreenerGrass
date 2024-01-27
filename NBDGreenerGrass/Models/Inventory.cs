using System.ComponentModel.DataAnnotations;

namespace NBDGreenerGrass.Models
{
    public class Inventory
    {
        public int InventoryID { get; set; }

        [Required(ErrorMessage = "Inventory Description is required")]
        [StringLength(255, ErrorMessage = "Inventory Description should be at most 255 characters")]
        public string InventoryDesc { get; set; }

        [Required(ErrorMessage = "Inventory Size is required")]
        [StringLength(50, ErrorMessage = "Inventory Size should be at most 50 characters")]
        public string InventorySize { get; set; }

        [Required(ErrorMessage = "Inventory Code is required")]
        [StringLength(20, ErrorMessage = "Inventory Code should be at most 20 characters")]
        public string InventoryCode { get; set; }

        [Required(ErrorMessage = "Inventory List Price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Inventory List Price must be greater than 0")]
        public decimal InventoryListPrice { get; set; }
    }
}
