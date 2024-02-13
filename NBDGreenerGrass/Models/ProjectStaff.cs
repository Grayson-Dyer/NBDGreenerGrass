namespace NBDGreenerGrass.Models
{
    public class ProjectStaff
    {
        public int ID { get; set; }
        public int ProjectID { get; set; }
        public Project Project { get; set; }

        public int StaffID { get; set; }
        public Staff Staff { get; set; }
    }
}
