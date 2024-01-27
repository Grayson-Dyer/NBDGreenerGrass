using System.ComponentModel.DataAnnotations;

namespace NBDGreenerGrass.Models
{
    public class Bid
    {
        
        public int ID { get; set; }

        [Required(ErrorMessage = "Bid Stage is required")]
        [StringLength(50, ErrorMessage = "Bid Stage should be at most 50 characters")]
        public string Stage { get; set; }

        [Required(ErrorMessage = "Staff ID is required")]
        public int StaffID { get; set; }

        [Required(ErrorMessage = "Project ID is required")]
        public int ProjectID { get; set; }
        public Project Project { get; set; }
        
        public ICollection<BidMaterial> BidMaterials { get; set; } = new HashSet<BidMaterial>();
        public ICollection<BidLabour> BidLabours { get; set; } = new HashSet<BidLabour>();

    }

}
