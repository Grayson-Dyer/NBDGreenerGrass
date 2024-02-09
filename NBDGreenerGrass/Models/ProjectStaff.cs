namespace NBDGreenerGrass.Models
{
    public class ProjectStaff
    {
        public int ProjectStaffID { get; set; }

        public int StaffID { get; set; }

        public int ProjectID { get; set; }

        public Staff Staff { get; set; }

        public Project Project { get; set; }
    }

}
