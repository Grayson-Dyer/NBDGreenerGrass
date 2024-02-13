using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace NBDGreenerGrass.Models
{
    

   

    public class Client : IValidatableObject
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Client Name is required")]
        [StringLength(100, ErrorMessage = "Client Name should be at most 100 characters")]
        public string ClientName { get; set; }

        [Required(ErrorMessage = "First Name of the contact person is required")]
        [StringLength(50, ErrorMessage = "First Name should be at most 50 characters")]
        public string ClientContactFirst { get; set; }

        [Required(ErrorMessage = "Last Name of the contact person is required")]
        [StringLength(50, ErrorMessage = "Last Name should be at most 50 characters")]
        public string ClientContactLast { get; set; }

        [Required(ErrorMessage = "Contact Role is required")]
        [StringLength(50, ErrorMessage = "Contact Role should be at most 50 characters")]
        public string ClientContactRole { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string ClientPhone { get; set; }

        [Required(ErrorMessage = "Client Address is required")]
        [StringLength(255, ErrorMessage = "Client Address should be at most 255 characters")]
        public string ClientAddress { get; set; }

        [Required(ErrorMessage = "Client Province is required")]
        [StringLength(50, ErrorMessage = "Client Province should be at most 50 characters")]
        public string ClientProvince { get; set; }

        [Required(ErrorMessage = "Client Postal Code is required")]
        [StringLength(20, ErrorMessage = "Client Postal Code should be at most 20 characters")]
        public string ClientPostal { get; set; }

        [Required(ErrorMessage = "Client City is required")]
        [StringLength(50, ErrorMessage = "Client City should be at most 50 characters")]
        public string ClientCity { get; set; }

        [Required(ErrorMessage = "Client Role ID is required")]
        public int ClientRoleID { get; set; }

        public ICollection<Project> Projects { get; set; } = new HashSet<Project>();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            // Custom validation logic
            if (string.IsNullOrWhiteSpace(ClientProvince))
            {
                results.Add(new ValidationResult("Client Province is required.", new[] { nameof(ClientProvince) }));
            }

            if (string.IsNullOrWhiteSpace(ClientPostal))
            {
                results.Add(new ValidationResult("Client Postal Code is required.", new[] { nameof(ClientPostal) }));
            }
            else if (!IsValidPostalCode(ClientPostal))
            {
                results.Add(new ValidationResult("Invalid Postal Code format.", new[] { nameof(ClientPostal) }));
            }

            // Add more custom validation as needed...

            return results;
        }

        // Custom method to validate postal code format (you can adjust this based on your requirements)
        private bool IsValidPostalCode(string postalCode)
        {
            // Implement your postal code validation logic here
            // Example: Check if it follows a specific pattern
            // For simplicity, assuming a format of "A1A 1A1" (letter, digit, letter, space, digit, letter, digit)
            return System.Text.RegularExpressions.Regex.IsMatch(postalCode, @"^[A-Za-z]\d[A-Za-z] \d[A-Za-z]\d$");
        }
    }


}
