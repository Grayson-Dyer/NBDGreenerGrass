using System.ComponentModel.DataAnnotations;

namespace NBDGreenerGrass.Models
{
    public class Labour
    {
        public int ID { get; set; }

        [Display(Name = "Labour Type")]
        [Required(ErrorMessage = "Labour Type is required")]
        public string LabourType { get; set; }

        [Display(Name = "Labour Price")]
        [Required(ErrorMessage = "Labour Price is required")]
        [DataType(DataType.Currency)]
        public decimal LabourPrice { get; set; }

        [Display(Name = "Labour Cost")]
        [Required(ErrorMessage = "Labour Cost is required")]
        [DataType(DataType.Currency)]
        [RegularExpression(@"^\d{1,6}(\.\d{1,2})?$", ErrorMessage = "Invalid format. Maximum 999999.99")]
        public decimal LabourCost { get; set; }


        public ICollection<BidLabour> BidLabours { get; set; } = new HashSet<BidLabour>();
    }
}
