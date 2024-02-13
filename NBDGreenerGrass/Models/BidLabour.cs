using System.ComponentModel.DataAnnotations;

namespace NBDGreenerGrass.Models
{
    using System.ComponentModel.DataAnnotations;

    public class BidLabour
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Labour ID is required")]
        public int LabourID { get; set; }
        public Labour Labour { get; set; }

        [Required(ErrorMessage = "Bid ID is required")]
        public int BidID { get; set; }
        public Bid Bid { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Hours Worked must be greater than 0")]
        public int? HoursWorked { get; set; }

        [Required(ErrorMessage = "Labour Type is required")]
        public string LabourType { get; set; }

        [Required(ErrorMessage = "Labour Price is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Labour Price must be greater than or equal to 0")]
        public double LabourPrice { get; set; }

        [Required(ErrorMessage = "Labour Cost is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Labour Cost must be greater than or equal to 0")]
        public double LabourCost { get; set; }
    }

}
