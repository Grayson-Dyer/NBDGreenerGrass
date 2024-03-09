using System.ComponentModel.DataAnnotations;
namespace NBDGreenerGrass.Models
{
    public class StaffRole
    {
        public int ID { get; set; }

        [Display(Name = "Role", Description = "Enter the role of the user")]
        [StringLength(50, ErrorMessage = "Role should be at most 50 characters")]
        [Required(ErrorMessage = "Role is required")]
        public string Role { get; set; }

        public ICollection<Staff> Staffs { get; set; } = new HashSet<Staff>(); 
    }
}
