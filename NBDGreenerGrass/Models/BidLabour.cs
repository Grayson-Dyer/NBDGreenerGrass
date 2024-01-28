using System.ComponentModel.DataAnnotations;

namespace NBDGreenerGrass.Models
{
    public class BidLabour
    {

        [Required(ErrorMessage = "Labour ID is required")]
        public int LabourID { get; set; }
        public Labour Labour { get; set; }

        [Required(ErrorMessage = "Bid ID is required")]
        public int BidID { get; set; }
        public Bid Bid { get; set; }

        [Range(0,int.MaxValue, ErrorMessage = "Hours Worked must be greater than 0")]
        public int? HoursWorked { get; set; }

    }
}
