public class BidMaterialViewModel
{
    public int BidID { get; set; }
    public IEnumerable<InventoryItem> AvailableInventory { get; set; }
    public List<SelectedInventoryItem> SelectedInventoryItems { get; set; }

    public BidMaterialViewModel()
    {
        SelectedInventoryItems = new List<SelectedInventoryItem>();
    }
}

public class InventoryItem
{
    public int ID { get; set; }
    public string InventoryDesc { get; set; }
    public string InventorySize { get; set; }
    public string InventoryCode { get; set; }
    public decimal InventoryListPrice { get; set; }
}

public class SelectedInventoryItem
{
    public int InventoryID { get; set; }
    public int Quantity { get; set; }
}