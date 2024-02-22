using System.ComponentModel.DataAnnotations;

namespace NBDGreenerGrass.Models
{
    public class ClientRole
    {
        public int ID { get; set; }

        [Display(Name = "Role")]
        [Required(ErrorMessage = "Role is required")]
        [StringLength(50, ErrorMessage = "Role should be at most 50 characters")]
        public string Role { get; set; }

        public ICollection<Client> Clients { get; set; } = new HashSet<Client>();
    }
}
