using System.ComponentModel.DataAnnotations;

namespace NBDGreenerGrass.Models
{
    using System.ComponentModel.DataAnnotations;

    public class BidLabour
    {
        //TODO: is this a composite key?
        public int ID { get; set; }

        [Display(Name = "Labour", Description = "Select the labor associated with this entry")]
        [Required(ErrorMessage = "Labour ID is required")]
        public int LabourID { get; set; }
        [Display(Name = "Labour")]
        public Labour Labour { get; set; }

        [Display(Name = "Bid", Description = "Select the bid associated with this entry")]
        [Required(ErrorMessage = "Bid ID is required")]
        public int BidID { get; set; }
        [Display(Name = "Bid")]
        public Bid Bid { get; set; }

        // Needs to be added to our model
        [Display(Name = "Hours Worked", Description = "Enter the number of hours worked")]
        [Range(0, int.MaxValue, ErrorMessage = "Hours Worked must be greater than 0")]
        public int? HoursWorked { get; set; }

        [Display(Name = "Labour Type", Description = "Specify the type of labor")]
        [Required(ErrorMessage = "Labour Type is required")]
        public string LabourType { get; set; }

        [Display(Name = "Labour Price", Description = "Enter the price for the labor")]
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Labour Price is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Labour Price must be greater than or equal to 0")]
        [RegularExpression(@"^\d{1,6}(\.\d{1,2})?$", ErrorMessage = "Invalid format. Maximum 999999.99")]
        public decimal LabourPrice { get; set; }

        // Needs to be a float(8,2) in our data model just to keep it the same as all other's
        [Display(Name = "Labour Cost", Description = "Enter the cost for the labor")]
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Labour Cost is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Labour Cost must be greater than or equal to 0")]
        [RegularExpression(@"^\d{1,6}(\.\d{1,2})?$", ErrorMessage = "Invalid format. Maximum 999999.99")]
        public decimal LabourCost { get; set; }

        public ICollection<Bid> Bids { get; set; }
    }

}
