using System.ComponentModel.DataAnnotations;

namespace NBDGreenerGrass.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Client
    {
        public int ClientID { get; set; }

        [Required(ErrorMessage = "Client Name is required")]
        [StringLength(100, ErrorMessage = "Client Name should be at most 100 characters")]
        public string ClientName { get; set; }

        [Required(ErrorMessage = "First Name of the contact person is required")]
        [StringLength(50, ErrorMessage = "First Name should be at most 50 characters")]
        public string ClientFirstName { get; set; }

        [Required(ErrorMessage = "Last Name of the contact person is required")]
        [StringLength(50, ErrorMessage = "Last Name should be at most 50 characters")]
        public string ClientLastName { get; set; }

        [Required(ErrorMessage = "Contact Role is required")]
        [StringLength(50, ErrorMessage = "Contact Role should be at most 50 characters")]
        public string ClientContactRole { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string ClientPhone { get; set; }

        [Required(ErrorMessage = "Client Address is required")]
        [StringLength(255, ErrorMessage = "Client Address should be at most 255 characters")]
        public string ClientAddress { get; set; }

        [Required(ErrorMessage = "Client Role ID is required")]
        public int ClientRoleID { get; set; }

        public ICollection<Project> Projects { get; set; } = new HashSet<Project>();
    }

}
