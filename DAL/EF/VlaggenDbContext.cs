using Microsoft.EntityFrameworkCore;
using Domain;

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
      optionsBuilder.UseSqlite("Data Source=Vlaggen_distr_DB.db;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
    }
  }
}