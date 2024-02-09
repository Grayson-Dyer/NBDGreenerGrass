using System.ComponentModel.DataAnnotations;

namespace NBDGreenerGrass.Models
{
    public class Project
    {
        public int ID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Project Start Date is required")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Project End Date is required")]
        public DateTime EndDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Date project was made is required")]
        public DateTime DateMade { get; set; }

        [Required(ErrorMessage = "Project Amount is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Project Amount must be greater than 0")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Client ID is required")]
        public int ClientID { get; set; }
        public Client Client { get; set; }

        [Display(Name = "Project Staff ID")]
        public int ProjectStaffID { get; set; }

        [Required(ErrorMessage = "Project Address is required")]
        [StringLength(255, ErrorMessage = "Project Address should be at most 255 characters")]
        public string ProjectAddress { get; set; }

        [Required(ErrorMessage = "Project Province is required")]
        [StringLength(50, ErrorMessage = "Project Province should be at most 50 characters")]
        public string ProjectProvince { get; set; }

        [Required(ErrorMessage = "Project Postal Code is required")]
        [StringLength(10, ErrorMessage = "Project Postal Code should be at most 10 characters")]
        public string ProjectPostal { get; set; }

        public ICollection<Bid> Bids { get; set; } = new HashSet<Bid>();
        public ICollection<ProjectStaff> ProjectStaffs { get; set; } = new HashSet<ProjectStaff>();
    }

}
