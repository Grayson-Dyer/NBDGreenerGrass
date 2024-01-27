using System.ComponentModel.DataAnnotations;

namespace NBDGreenerGrass.Models
{
    public class Bid
    {
        public int BidID { get; set; }

        [Required(ErrorMessage = "Bid Stage is required")]
        [StringLength(50, ErrorMessage = "Bid Stage should be at most 50 characters")]
        public string BidStage { get; set; }

        [Required(ErrorMessage = "Staff ID is required")]
        public int StaffID { get; set; }

        [Required(ErrorMessage = "Bid Materials ID is required")]
        public int BidMaterialsID { get; set; }

        [Required(ErrorMessage = "Bid Labour ID is required")]
        public int BidLabourID { get; set; }
    }

}
