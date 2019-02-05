using Microsoft.EntityFrameworkCore;
using ODT.Models;

namespace DAL.EF
{
  public class VlaggenDbContext : DbContext
  {
    public DbSet<Vlag> Vlags { get; set; }

    public VlaggenDbContext()
    {
      VlaggenInitializer.Initialize(this, true);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseSqlite("Data Source=Vlaggen_DB.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
    }
  }
}