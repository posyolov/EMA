using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Repository.EF
{
    public class EquipmentContext : DbContext
    {
        public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder => { builder.AddDebug(); });

        public EquipmentContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLoggerFactory(MyLoggerFactory)
                .UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=EmaDb;AttachDbFilename='C:\Temp\Projects\DB\EMA\EmaDb.mdf';Trusted_Connection=True;");
        }

        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<CatalogItem> Catalog { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<EntryReason> EntryReasons { get; set; }
        public DbSet<EntryContinuationCriteria> EntryContinuationCriterias { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Entry> Entries { get; set; }

    }
}
