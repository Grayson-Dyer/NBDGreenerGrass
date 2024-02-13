using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace NBDGreenerGrass.Models
{


    public class Project : IValidatableObject
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Client ID is required")]
        public int ClientID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Project Start Date is required")]
        public DateTime ProjectStart { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Project End Date is required")]
        public DateTime ProjectEnd { get; set; }

        [Required(ErrorMessage = "Project Amount is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Project Amount must be greater than 0")]
        public decimal ProjectAmount { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Date project was made is required")]
        public DateTime ProjectDate { get; set; }

        public int ProjectStaffID { get; set; }

        [Required(ErrorMessage = "Project Address is required")]
        [StringLength(255, ErrorMessage = "Project Address should be at most 255 characters")]
        public string ProjectAddress { get; set; }

        [Required(ErrorMessage = "Project Province is required")]
        [StringLength(50, ErrorMessage = "Project Province should be at most 50 characters")]
        public string ProjectProvince { get; set; }

        [Required(ErrorMessage = "Project Postal Code is required")]
        [StringLength(20, ErrorMessage = "Project Postal Code should be at most 20 characters")]
        public string ProjectPostal { get; set; }

        [Required(ErrorMessage = "Project City is required")]
        [StringLength(50, ErrorMessage = "Project City should be at most 50 characters")]
        public string ProjectCity { get; set; }

        public Client Client { get; set; }

        public ICollection<Bid> Bids { get; set; } = new HashSet<Bid>();

        public ICollection<ProjectStaff> ProjectStaffs { get; set; } = new HashSet<ProjectStaff>();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            // Custom validation logic
            if (ProjectStart > ProjectEnd)
            {
                results.Add(new ValidationResult("Project Start Date must be before Project End Date.", new[] { nameof(ProjectStart), nameof(ProjectEnd) }));
            }

            // Add more custom validation as needed...

            return results;
        }
    }

}
