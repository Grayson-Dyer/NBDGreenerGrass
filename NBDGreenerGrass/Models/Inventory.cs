using System.ComponentModel.DataAnnotations;

namespace NBDGreenerGrass.Models
{
    public class Inventory
    {

        public int ID { get; set; }
        
        public string InventoryDesc { get; set; }
       
        public string InventorySize { get; set; }
       
        public string InventoryCode { get; set; }
       
        public decimal InventoryListPrice { get; set; }
    }
}
