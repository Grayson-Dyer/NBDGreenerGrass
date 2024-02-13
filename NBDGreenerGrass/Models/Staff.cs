using System.ComponentModel.DataAnnotations;

namespace NBDGreenerGrass.Models
{
    public class Staff
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [StringLength(50, ErrorMessage = "First Name should be at most 50 characters")]
        public string StaffFirst { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(50, ErrorMessage = "Last Name should be at most 50 characters")]
        public string StaffLast { get; set; }

        [Required(ErrorMessage = "Staff Role is required")]
        [StringLength(50, ErrorMessage = "Staff Role should be at most 50 characters")]
        public string StaffRole { get; set; }

        public ICollection<Bid> Bids { get; set; } = new HashSet<Bid>();

        public ICollection<ProjectStaff> projectStaffs { get; set; } = new HashSet<ProjectStaff>();
    }
}
