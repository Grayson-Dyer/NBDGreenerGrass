using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace NBDGreenerGrass.Models
{
    

   

    public class Client
    {
        public int ID { get; set; }

        [Display(Name = "Client Name")]
        [Required(ErrorMessage = "Client Name is required")]
        [StringLength(100, ErrorMessage = "Client Name should be at most 100 characters")]
        public string ClientName { get; set; }

        [Display(Name = "Contact First Name")]
        [Required(ErrorMessage = "First Name of the contact person is required")]
        [StringLength(50, ErrorMessage = "First Name should be at most 50 characters")]
        public string ClientContactFirst { get; set; }

        [Display(Name = "Contact Last Name")]
        [Required(ErrorMessage = "Last Name of the contact person is required")]
        [StringLength(50, ErrorMessage = "Last Name should be at most 50 characters")]
        public string ClientContactLast { get; set; }

        [Display(Name = "Contact Phone Number")]
        [Required(ErrorMessage = "Contact Phone Number is required")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid phone number")]
        [RegularExpression(@"^\+?(\d{1,4})?[-.\s]?\(?\d{1,4}\)?[-.\s]?\d{1,4}[-.\s]?\d{1,9}$", ErrorMessage = "Invalid phone number format")]
        public string ClientPhone { get; set; }

        [Display(Name = "Client Street")]
        [Required(ErrorMessage = "Client Street is required")]
        [StringLength(255, ErrorMessage = "Client Street should be at most 255 characters")]
        public string ClientStreet { get; set; }

        [Display(Name = "Client City")]
        [Required(ErrorMessage = "Client City is required")]
        [StringLength(50, ErrorMessage = "Client City should be at most 50 characters")]
        public string ClientCity { get; set; }

        [Display(Name = "Client Postal Code")]
        [Required(ErrorMessage = "Client Postal Code is required")]
        [StringLength(20, ErrorMessage = "Client Postal Code should be at most 20 characters")]
        // Canadian postal code format (D F I O Q U V are gone on purpose)
        [RegularExpression(@"^[ABCEGHJKLMNPRSTVXY]\d[ABCEGHJKLMNPRSTVWXYZ] ?\d[ABCEGHJKLMNPRSTVWXYZ]\d$", ErrorMessage = "Please enter a valid Canadian postal code.")] 
        public string ClientPostal { get; set; }

        [Display(Name = "Client Province")]
        [Required(ErrorMessage = "Client Province is required")]
        [StringLength(50, ErrorMessage = "Client Province should be at most 50 characters")]
        public string ClientProvince { get; set; }

        [Display(Name = "Contact Role")]
        [Required(ErrorMessage = "Contact Role is required")]
        public int ClientRoleID { get; set; }

        [Display(Name = "Contact Role")]
        public ClientRole ClientRole { get; set; }

        //Removed validation because it can be implemented in the via DataAnnotations

        public ICollection<Project> Projects { get; set; } = new HashSet<Project>();

    }


}
