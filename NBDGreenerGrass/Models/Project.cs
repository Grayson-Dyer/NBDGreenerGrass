using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace NBDGreenerGrass.Models
{


    public class Project : IValidatableObject
    {
        public int ID { get; set; }

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Project Start Date is required")]
        public DateTime ProjectStart { get; set; }

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Project End Date is required")]
        public DateTime ProjectEnd { get; set; }

        [Display(Name = "Project Cost")]
        [Required(ErrorMessage = "Project Amount is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Project Amount must be greater than 0")]
        [DataType(DataType.Currency)]
        [RegularExpression(@"^\d{1,8}(\.\d{1,2})?$", ErrorMessage = "Invalid format. Maximum 9999999.99")]
        public decimal ProjectAmount { get; set; }


        [Display(Name = "Project Date Created")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Date project was made is required")]
        public DateTime ProjectDate { get; set; }

        [Display(Name = "Additonal Project Notes")]
        [StringLength(255, ErrorMessage = "Project Notes should be at most 255 characters")]
        [DataType(DataType.MultilineText)]
        public string ProjectNotes { get; set; }

        [Display(Name = "Project Street")]
        [Required(ErrorMessage = "Project Street is required")]
        [StringLength(255, ErrorMessage = "Project Street should be at most 255 characters")]
        public string ProjectStreet { get; set; }

        [Display(Name = "Project Province")]
        [Required(ErrorMessage = "Project Province is required")]
        [StringLength(50, ErrorMessage = "Project Province should be at most 50 characters")]
        public string ProjectProvince { get; set; }

        [Display(Name = "Project Postal Code")]
        [Required(ErrorMessage = "Project Postal Code is required")]
        [StringLength(20, ErrorMessage = "Project Postal Code should be at most 20 characters")]
        [RegularExpression(@"^[ABCEGHJKLMNPRSTVXY]\d[ABCEGHJKLMNPRSTVWXYZ] ?\d[ABCEGHJKLMNPRSTVWXYZ]\d$", ErrorMessage = "Please enter a valid Canadian postal code.")]
        public string ProjectPostal { get; set; }

        [Display(Name = "Project City")]
        [Required(ErrorMessage = "Project City is required")]
        [StringLength(50, ErrorMessage = "Project City should be at most 50 characters")]
        public string ProjectCity { get; set; }

        [Display(Name = "Client")]
        [Required(ErrorMessage = "Client is required")]
        public int ClientID { get; set; }

        [Display(Name = "Client")]
        public Client Client { get; set; }


        public ICollection<Bid> Bids { get; set; } = new HashSet<Bid>();

        public ICollection<BidStaff> ProjectStaffs { get; set; } = new HashSet<BidStaff>();

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
