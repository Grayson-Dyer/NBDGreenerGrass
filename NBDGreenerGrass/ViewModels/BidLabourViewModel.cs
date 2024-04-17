public class BidLabourViewModel
{
    public int BidID { get; set; }
    public IEnumerable<LabourItem> AvailableLabourTypes { get; set; }
    public List<SelectedLabour> SelectedLabourTypes { get; set; }
    public decimal ProjectCost { get; set;}

    // Sorting & Filtering
    public string SearchLabourType { get; set; }
    public string CurrentFilter { get; set; }
    public string LabourTypeSort { get; set; }
    public string LabourPriceSort { get; set; }
    public string LabourCostSort { get; set; }

    public BidLabourViewModel()
    {
        SelectedLabourTypes = new List<SelectedLabour>();
    }
}

public class LabourItem
{
    public int ID { get; set; }
    public string LabourType { get; set; }
    public decimal LabourPrice { get; set; }
    public decimal LabourCost { get; set; }
}

public class SelectedLabour
{
    public int LabourID { get; set; }
    public int HoursWorked { get; set; }
}