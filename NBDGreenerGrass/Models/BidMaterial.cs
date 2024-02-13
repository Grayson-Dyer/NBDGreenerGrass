using System.ComponentModel.DataAnnotations;

namespace NBDGreenerGrass.Models
{
    using System.ComponentModel.DataAnnotations;

    public class BidMaterial
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Inventory ID is required")]
        public int InventoryID { get; set; }
        public Inventory Inventory { get; set; }

        [Required(ErrorMessage = "Inventory Description is required")]
        public string InventoryDesc { get; set; }

        [Required(ErrorMessage = "Inventory Size is required")]
        public string InventorySize { get; set; }

        [Required(ErrorMessage = "Inventory Code is required")]
        public string InventoryCode { get; set; }

        [Required(ErrorMessage = "Inventory List Price is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Inventory List Price must be greater than or equal to 0")]
        public double InventoryListPrice { get; set; }
    }

}
