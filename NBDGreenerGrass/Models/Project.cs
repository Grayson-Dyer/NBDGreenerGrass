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

        [Required(ErrorMessage = "Project Location is required")]
        [StringLength(100, ErrorMessage = "Project Location should be at most 100 characters")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Project Amount is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Project Amount must be greater than 0")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Client ID is required")]
        public int ClientID { get; set; }
        public Client Client { get; set; }

        public ICollection<Bid> Bids { get; set; } = new HashSet<Bid>();  

        public ICollection<ProjectStaff> ProjectStaffs { get; set; } = new HashSet<ProjectStaff>();

    }
}
