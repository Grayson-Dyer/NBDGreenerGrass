using System.ComponentModel.DataAnnotations;

namespace NBDGreenerGrass.Models
{
    public class Labour
    {
        public int ID { get; set; }
        
        public string LabourType { get; set; }
       
        public decimal LabourPrice { get; set; }
       
        public decimal LabourCost { get; set; }
    }
}
