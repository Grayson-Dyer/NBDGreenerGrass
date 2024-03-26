using Microsoft.EntityFrameworkCore;
using NBDGreenerGrass.Models;
namespace NBDGreenerGrass.Data
{
    public class NBDContext : DbContext
    {
        public NBDContext(DbContextOptions<NBDContext> options) : base(options)
        {
        }

        public DbSet<StaffRole> StaffRoles { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Labour> Labours { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<BidLabour> BidLabours { get; set; }
        public DbSet<BidMaterial> BidMaterials { get; set; }
        //public DbSet<BidStaff> Bidstaffs { get; set; }
        public DbSet<ClientRole> ClientRoles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BidMaterial>()
                .HasKey(bm => new { bm.BidID, bm.InventoryID });
            modelBuilder.Entity<BidLabour>()
                .HasKey(bl => new { bl.BidID, bl.LabourID });


            modelBuilder.Entity<Project>()
                .HasOne(p => p.Client)
                .WithMany(c => c.Projects)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Bid>()
                .HasMany(bl => bl.BidLabours)
                .WithOne(b => b.Bid);
            modelBuilder.Entity<Bid>()
                .HasMany(bm => bm.BidMaterials)
                .WithOne(b => b.Bid);
            // BidStaff no longer exists
            //modelBuilder.Entity<BidStaff>()
            //    .HasKey(ps => new { ps.ProjectID, ps.StaffID });


        }
    }
}
