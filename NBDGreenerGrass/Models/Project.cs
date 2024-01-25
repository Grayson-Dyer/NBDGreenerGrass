using System.ComponentModel.DataAnnotations;

namespace NBDGreenerGrass.Models
{
    public class Project
    {
        public int ProjectID { get; set; }

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

        [Required(ErrorMessage = "Project Location is required")]
        [StringLength(100, ErrorMessage = "Project Location should be at most 100 characters")]
        public string ProjectLocation { get; set; }

        [Required(ErrorMessage = "Project Amount is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Project Amount must be greater than 0")]
        public decimal ProjectAmount { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Project Date is required")]
        public DateTime ProjectDate { get; set; }

        [Required(ErrorMessage = "Bid ID is required")]
        public int BidID { get; set; }

        [Required(ErrorMessage = "Staff ID is required")]
        public int StaffID { get; set; }
    }
}
