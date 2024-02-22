using System.ComponentModel.DataAnnotations;

namespace NBDGreenerGrass.Models
{
    public class Inventory
    {
        
        public int ID { get; set; }

        //TODO: Shouldnt there be an inventory name? = Cross ref the model with the excel we use Code & Desc for that.
        [Display(Name = "Inventory Desc")]
        public string InventoryDesc { get; set; }

        // TODO: Should this be a string or a number? = In the model, it's a float(8,2) 
        [Display(Name = "Inventory Size")]
        [Required(ErrorMessage = "Inventory Size is required")]
        [RegularExpression(@"^\d{1,6}(\.\d{1,2})?$", ErrorMessage = "Invalid format. Maximum 999999.99")]
        public decimal? InventorySize { get; set; }

        [Display(Name = "Inventory List Price")]
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Inventory List Price is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Inventory List Price must be greater than or equal to 0")]
        [RegularExpression(@"^\d{1,6}(\.\d{1,2})?$", ErrorMessage = "Invalid format. Maximum 999999.99")]
        public decimal InventoryListPrice { get; set; }
        // This was a decimal in the original code but I changed it to a double to match the other BidMaterial properties = changed back for precision / also changed in BidMaterial 

        [Display(Name = "Inventory Code")]
        [Required(ErrorMessage = "Inventory Code is required")]
        [StringLength(50, ErrorMessage = "Inventory Code should be at most 50 characters")]
        public string InventoryCode { get; set; }


        public ICollection<BidMaterial> BidMaterials { get; set; }
    }
}
