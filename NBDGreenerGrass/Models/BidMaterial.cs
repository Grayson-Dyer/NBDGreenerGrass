using System.ComponentModel.DataAnnotations;

namespace NBDGreenerGrass.Models
{
    using System.ComponentModel.DataAnnotations;

    public class BidMaterial
    {
        //TODO: Is this a composite key?
        public int ID { get; set; }

        [Display(Name = "Bid")]
        [Required(ErrorMessage = "Bid ID is required")]
        public int BidID { get; set; }

        [Display(Name = "Bid")]
        public Bid Bid { get; set; }

        [Display(Name = "Inventory")]
        [Required(ErrorMessage = "Inventory ID is required")]
        public int InventoryID { get; set; }

        [Display(Name = "Inventory")]  
        public Inventory Inventory { get; set; }

        [Display(Name = "Inventory Description")]
        [StringLength(75, ErrorMessage = "Inventory Description should be at most 75 characters")]
        [Required(ErrorMessage = "Inventory Description is required")]
        public string InventoryDesc { get; set; }

        //TODO: is this a string or a number???
        [Display(Name = "Inventory Size")]
        [StringLength(50, ErrorMessage = "Inventory Size should be at most 50 characters")]
        [Required(ErrorMessage = "Inventory Size is required")]
        public string InventorySize { get; set; }

        [Display(Name = "Inventory Code")]
        [StringLength(50, ErrorMessage = "Inventory Code should be at most 50 characters")]
        [Required(ErrorMessage = "Inventory Code is required")]
        public string InventoryCode { get; set; }

        [Display(Name = "Inventory List Price")]
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Inventory List Price is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Inventory List Price must be greater than or equal to 0")]
        public double InventoryListPrice { get; set; }


        public ICollection<Bid> Bids { get; set; }
    }

}
