using System.ComponentModel.DataAnnotations;

namespace NBDGreenerGrass.Models
{
    using System.ComponentModel.DataAnnotations;

    public class BidLabour
    {
        //TODO: is this a composite key?
        public int ID { get; set; }

        [Display(Name = "Labour")]
        [Required(ErrorMessage = "Labour ID is required")]
        public int LabourID { get; set; }
        [Display(Name = "Labour")]
        public Labour Labour { get; set; }

        [Display(Name = "Bid")]
        [Required(ErrorMessage = "Bid ID is required")]
        public int BidID { get; set; }
        [Display(Name = "Bid")]
        public Bid Bid { get; set; }

        [Display(Name = "Hours Worked")]
        [Range(0, int.MaxValue, ErrorMessage = "Hours Worked must be greater than 0")]
        public int? HoursWorked { get; set; }

        [Display(Name = "Labour Type")]
        [Required(ErrorMessage = "Labour Type is required")]
        public string LabourType { get; set; }

        [Display(Name = "Labour Price")]
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Labour Price is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Labour Price must be greater than or equal to 0")]
        public double LabourPrice { get; set; }

        [Display(Name = "Labour Cost")]
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Labour Cost is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Labour Cost must be greater than or equal to 0")]
        public double LabourCost { get; set; }

        public ICollection<Bid> Bids { get; set; }
    }

}
