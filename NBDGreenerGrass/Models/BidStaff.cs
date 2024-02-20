using System.ComponentModel.DataAnnotations;
namespace NBDGreenerGrass.Models
{
    public class BidStaff
    {
 
        [Required(ErrorMessage = "Project ID is required")]
        public int ProjectID { get; set; }
        public Project Project { get; set; }

        [Required(ErrorMessage = "Staff ID is required")]
        public int StaffID { get; set; }
        public Staff Staff { get; set; }
    }
}
