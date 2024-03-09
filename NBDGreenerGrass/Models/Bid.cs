using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using NBDGreenerGrass.Enums;

namespace NBDGreenerGrass.Models
{
    public class Bid
    {
        
        public int ID { get; set; }

        [Display(Name = "Bid Stage", Description = "Select the current bid stage")]
        [Required(ErrorMessage = "Bid Stage is required")]
        [EnumDataType(typeof(BidStage), ErrorMessage = "Invalid Bid Stage")]
        public BidStage Stage { get; set; } = BidStage.Unapproved;

        [Display(Name = "Date Made", Description = "Enter the date the bid was made")]
        [Required(ErrorMessage = "Date Made is required")]
        [DataType(DataType.Date)]
        public DateTime DateMade { get; set; } = DateTime.Now;

        [Display(Name = "Description", Description = "Provide a brief description of the bid")]
        [DataType(DataType.MultilineText)]
        [StringLength(1500, ErrorMessage = "Description cannot be more than 500 characters")]
        public string Description { get; set; }

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
        //public ICollection<BidStaff> BidStaffs { get; set; } = new HashSeWt<BidStaff>();


        /// <summary>
        /// Demotes the bid to unapproved
        /// </summary>
        public void BidDemote()
        {
            Stage = BidStage.Unapproved;
        }

        /// <summary>
        /// Promotes the bid to reviewed
        /// </summary>
        public void BidReviewed()
        {
            Stage = BidStage.Reviewed;
        }

        /// <summary>
        /// Promotes the bid to approved
        /// </summary>
        public void BidApproved()
        {
            Stage = BidStage.Approved;
        }
    }

}
