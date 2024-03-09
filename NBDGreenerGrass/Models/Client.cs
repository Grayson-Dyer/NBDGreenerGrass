using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace NBDGreenerGrass.Models
{
    

   

    public class Client
    {
        public int ID { get; set; }

        [Display(Name = "Client Name", Description = "Enter the client's full name")]
        [Required(ErrorMessage = "Client Name is required")]
        [StringLength(100, ErrorMessage = "Client Name should be at most 100 characters")]
        public string Name { get; set; }

        [Display(Name = "Contact First Name", Description = "Enter the contact person's first name")]
        [Required(ErrorMessage = "First Name of the contact person is required")]
        [StringLength(50, ErrorMessage = "First Name should be at most 50 characters")]
        public string ContactFirst { get; set; }

        [Display(Name = "Contact Last Name", Description = "Enter the contact person's last name")]
        [Required(ErrorMessage = "Last Name of the contact person is required")]
        [StringLength(50, ErrorMessage = "Last Name should be at most 50 characters")]
        public string ContactLast { get; set; }

        // See note below = we can dumb this down a wee bit if we have to...
        [Display(Name = "Contact Phone Number", Description = "Enter the contact phone number (e.g., +1 (555) 123-4567)")]
        [Required(ErrorMessage = "Contact Phone Number is required")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid phone number")]
        [RegularExpression(@"^\+?(\d{1,4})?[-.\s]?\(?\d{1,4}\)?[-.\s]?\d{1,4}[-.\s]?\d{1,9}$", ErrorMessage = "Invalid phone number format")]
        public string Phone { get; set; }

        [Display(Name = "Client Street", Description = "Enter the client's street address")]
        [Required(ErrorMessage = "Client Street is required")]
        [StringLength(255, ErrorMessage = "Client Street should be at most 255 characters")]
        public string Street { get; set; }

        [Display(Name = "Client City", Description = "Enter the client's city")]
        [Required(ErrorMessage = "Client City is required")]
        [StringLength(50, ErrorMessage = "Client City should be at most 50 characters")]
        public string City { get; set; }

        [Display(Name = "Client Postal Code", Description = "Example: G5T 5Y7")]
        [Required(ErrorMessage = "Client Postal Code is required")]
        [StringLength(20, ErrorMessage = "Client Postal Code should be at most 20 characters")]
        // Canadian postal code format (D F I O Q U V are gone on purpose)
        [RegularExpression(@"^[ABCEGHJKLMNPRSTVXY]\d[ABCEGHJKLMNPRSTVWXYZ] ?\d[ABCEGHJKLMNPRSTVWXYZ]\d$", ErrorMessage = "Please enter a valid Canadian postal code.")] 
        public string Postal { get; set; }

        [Display(Name = "Client Province", Description = "Enter the client's province")]
        [Required(ErrorMessage = "Client Province is required")]
        [StringLength(50, ErrorMessage = "Client Province should be at most 50 characters")]
        public string Province { get; set; }

        [Display(Name = "Contact Role")]
        [Required(ErrorMessage = "Contact Role is required")]
        public int ClientRoleID { get; set; }

        [Display(Name = "Contact Role")]
        public ClientRole ClientRole { get; set; }

        //Removed validation because it can be implemented in the via DataAnnotations

        public ICollection<Project> Projects { get; set; } = new HashSet<Project>();

    }


}

//In this example, the DataType(DataType.PhoneNumber) attribute is used to indicate that the property represents a phone number. 
//The RegularExpression attribute is then used to enforce a specific pattern for phone numbers, allowing for common formats while still being flexible. 
//This regular expression allows for international numbers, optional country code, parentheses, dashes, spaces, and dots. 
//You can adjust the regular expression to fit your specific formatting requirements if needed.

//Valid phone number examples that match the specified pattern:

//    +1 555-123-4567
//    555.123.4567
//    (555) 123-4567
//    123-4567

//This regular expression is designed to handle common variations in phone number formats, allowing for flexibility while still enforcing a general structure. 
//Adjust the regular expression according to your specific requirements.
