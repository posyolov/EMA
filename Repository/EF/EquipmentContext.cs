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
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=EmaDb;AttachDbFilename='C:\TEMP\PrjVS\_DB\EmaDb.mdf';Trusted_Connection=True;");
        }

        public DbSet<CatalogItem> Catalog { get; set; }
    }
}
