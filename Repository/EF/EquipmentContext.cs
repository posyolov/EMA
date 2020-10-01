using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Collections.Generic;
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
                //.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=EmaDb;AttachDbFilename='C:\Temp\Projects\DB\EMA\EmaDb.mdf';Trusted_Connection=True;");
                //.UseSqlServer(@"Data Source=DESKTOP-6ALK028\SQLEXPRESS;Initial Catalog=EmaDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                .UseSqlServer(@"Data Source=DESKTOP-TAQL9JS;Initial Catalog=EmaDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<CatalogItem> Catalog { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<EntryReason> EntryReasons { get; set; }
        public DbSet<EntryContinuationCriteria> EntryContinuationCriterias { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Entry> Entries { get; set; }
        public DbSet<EntryUser> EntryUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EntryUser>()
                .HasKey(t => new { t.EntryId, t.UserId });

            modelBuilder.Entity<EntryUser>()
                .HasOne(sc => sc.Entry)
                .WithMany(s => s.AssignedUsers)
                .HasForeignKey(sc => sc.EntryId);

            modelBuilder.Entity<EntryUser>()
                .HasOne(sc => sc.User)
                .WithMany(c => c.RelatedEntries)
                .HasForeignKey(sc => sc.UserId);
        }
    }
}
