using System.ComponentModel.DataAnnotations;

namespace NBDGreenerGrass.Models
{
    public class Client
    {
        public int ID { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name of the contact person is required")]
        [StringLength(50, ErrorMessage = "First Name should be at most 50 characters")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name of the contact person is required")]
        [StringLength(50, ErrorMessage = "Last Name should be at most 50 characters")]
        public string LastName { get; set; }

        [Display(Name = "Contact Role")]
        [Required(ErrorMessage = "Contact Role is required")]
        [StringLength(50, ErrorMessage = "Contact Role should be at most 50 characters")]
        public string ContactRole { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Client Address is required")]
        [StringLength(255, ErrorMessage = "Client Address should be at most 255 characters")]
        public string Address { get; set; }

        public ICollection<Project> Projects { get; set; } = new HashSet<Project>();
    }
}
