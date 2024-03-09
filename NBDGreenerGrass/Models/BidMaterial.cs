using System.ComponentModel.DataAnnotations;

namespace NBDGreenerGrass.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

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

        //TODO: is this a string or a number??? = Looks like booth see note below 
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
        [RegularExpression(@"^\d{1,6}(\.\d{1,2})?$", ErrorMessage = "Invalid format. Maximum 999999.99")]
        public decimal InventoryListPrice { get; set; }

        public ICollection<Bid> Bids { get; set; }

        // Additional property to store the numeric value if applicable
        public float? NumericInventorySize
        {
            get
            {
                // Try parsing the numeric part from the InventorySize string
                if (float.TryParse(Regex.Match(InventorySize, @"\d+(\.\d+)?").Value, out float result))
                {
                    return result;
                }
                return null; // Return null if parsing fails
            }
        }
        
    }

}



//If your model indicates float(8,2) for InventorySize, but in your Excel data, it's a mix of numbers and text (e.g., "15 gal"), and you want 
//to handle both cases, you can keep the property as a string and handle the parsing or validation logic in your code.

//In this example, InventorySize remains a string property, and a separate property NumericInventorySize is added to store the numeric value if applicable. 
//The Regex.Match is used to extract the numeric part from the string, and float.TryParse is used to attempt parsing it into a numeric value.

//This approach allows you to handle cases where InventorySize contains both numeric and text values.
