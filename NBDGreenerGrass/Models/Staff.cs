using System.ComponentModel.DataAnnotations;

namespace NBDGreenerGrass.Models
{
    public class Staff
    {
        public int ID { get; set; }

        [Display(Name = "First Name", Description = "Enter the first name of the staff")]
        [Required(ErrorMessage = "First Name is required")]
        [StringLength(50, ErrorMessage = "First Name should be at most 50 characters")]
        public string StaffFirst { get; set; }

        [Display(Name = "Last Name", Description = "Enter the last name of the staff")]
        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(50, ErrorMessage = "Last Name should be at most 50 characters")]
        public string StaffLast { get; set; }

        [Display(Name = "Role", Description = "Select the role of the staff")]
        [Required(ErrorMessage = "Staff Role is required")]
        public int StaffRoleID { get; set; }

        [Display(Name = "Role")]
        public StaffRole StaffRole { get; set; }

        // BidStaff no longer exists
        //public ICollection<BidStaff> BidStaffs { get; set; } = new HashSet<BidStaff>();
    }
}
