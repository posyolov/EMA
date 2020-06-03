using Microsoft.EntityFrameworkCore;

namespace Repository.EF
{
    public class EquipmentContext : DbContext
    {
        public EquipmentContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=EmaDb;AttachDbFilename='C:\Temp\Projects\DB\EMA\EmaDb.mdf';Trusted_Connection=True;");
        }

        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<CatalogItem> Catalog { get; set; }
    }
}
