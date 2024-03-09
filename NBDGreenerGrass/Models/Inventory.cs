using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace NBDGreenerGrass.Models
{
    public class Inventory
    {
        
        public int ID { get; set; }

        //TODO: Shouldnt there be an inventory name? = Cross ref the model with the excel we use Code & Desc for that.
        [Display(Name = "Inventory Desc", Description = "Enter a brief description of the inventory")]
        public string InventoryDesc { get; set; }

        // TODO: Should this be a string or a number? = Looks like booth see note below 
        [Display(Name = "Inventory Size", Description = "Enter the size of the inventory")]
        [StringLength(50, ErrorMessage = "Inventory Size should be at most 50 characters")]
        [Required(ErrorMessage = "Inventory Size is required")]
        public string InventorySize { get; set; }

        [Display(Name = "Inventory Code", Description = "Enter the code associated with the inventory")]
        [Required(ErrorMessage = "Inventory Code is required")]
        [StringLength(50, ErrorMessage = "Inventory Code should be at most 50 characters")]
        public string InventoryCode { get; set; }

        [Display(Name = "Inventory List Price", Description = "Enter the list price of the inventory")]
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Inventory List Price is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Inventory List Price must be greater than or equal to 0")]
        [RegularExpression(@"^\d{1,6}(\.\d{1,2})?$", ErrorMessage = "Invalid format. Maximum 999999.99")]
        public decimal InventoryListPrice { get; set; } // This was a decimal in the original code but I changed it to a double to match the other BidMaterial properties = changed back for precision / also changed in BidMaterial 

        public ICollection<BidMaterial> BidMaterials { get; set; }

        // Additional property to store the numeric value if applicable
        public decimal? NumericInventorySize
        {
            get
            {
                // Try parsing the numeric part from the InventorySize string
                if (decimal.TryParse(Regex.Match(InventorySize, @"\d+(\.\d+)?").Value, out decimal result))
                {
                    return result;
                }
                return null; // Return null if parsing fails
            }
        }
        
    }
}


//If your InventorySize property is specified as float(8,2) in your model but in practice, it contains both numeric values 
//and text (like "15 gal"), and you want to handle both cases, you may need to keep the property as a string and implement logic to handle parsing or validation.

//In this example, InventorySize remains a string property, and a separate property NumericInventorySize is added to store the numeric value if applicable. 
//The Regex.Match is used to extract the numeric part from the string, and decimal.TryParse is used to attempt parsing it into a numeric value.

//This approach allows you to handle cases where InventorySize contains both numeric and text values.
