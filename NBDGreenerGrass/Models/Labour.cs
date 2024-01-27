using System.ComponentModel.DataAnnotations;

namespace NBDGreenerGrass.Models
{
    public class Labour
    {
        public int LabourID { get; set; }

        [Required(ErrorMessage = "Labour Type is required")]
        [StringLength(50, ErrorMessage = "Labour Type should be at most 50 characters")]
        public string LabourType { get; set; }

        [Required(ErrorMessage = "Labour Price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Labour Price must be greater than 0")]
        public decimal LabourPrice { get; set; }

        [Required(ErrorMessage = "Labour Cost is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Labour Cost must be greater than 0")]
        public decimal LabourCost { get; set; }
    }
}
