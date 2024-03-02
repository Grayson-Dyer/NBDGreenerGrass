using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using NBDGreenerGrass.Enums;

namespace NBDGreenerGrass.Models
{
    public class Bid
    {
        
        public int ID { get; set; }

        [Display(Name = "Bid Stage")]
        [Required(ErrorMessage = "Bid Stage is required")]
        [EnumDataType(typeof(BidStage), ErrorMessage = "Invalid Bid Stage")]
        public BidStage Stage { get; set; } = BidStage.Unapproved;

        // Get rid of these
        //[Display(Name = "Bid Staff")]
        //[Required(ErrorMessage = "BidStaff ID is required")]
        //public int BidStaffID { get; set; }

        // Bid Staff no longer exists
        //[Display(Name = "Bid Staff")]
        //public BidStaff BidStaff { get; set; }

        [Display(Name = "Project")]
        [Required(ErrorMessage = "Project ID is required")]
        public int ProjectID { get; set; }

        [Display(Name = "Project")]
        public Project Project { get; set; }
        
        public ICollection<BidMaterial> BidMaterials { get; set; } = new HashSet<BidMaterial>();
        public ICollection<BidLabour> BidLabours { get; set; } = new HashSet<BidLabour>();
        //public ICollection<BidStaff> BidStaffs { get; set; } = new HashSet<BidStaff>();


        public void BidReviewed()
        {
            Stage = BidStage.Reviewed;
        }

        public void BidApproved()
        {
            Stage = BidStage.Approved;
        }
    }

}
