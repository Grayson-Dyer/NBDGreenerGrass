using System.ComponentModel.DataAnnotations;

namespace NBDGreenerGrass.Models
{
    public class ClientRole
    {
        [Key]
        public int ClientRoleID { get; set; }

        [Required(ErrorMessage = "Role is required")]
        [StringLength(50, ErrorMessage = "Role should be at most 50 characters")]
        public string Role { get; set; }
    }
}
