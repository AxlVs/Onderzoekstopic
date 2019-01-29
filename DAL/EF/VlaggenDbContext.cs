using Microsoft.EntityFrameworkCore;
using ODT.Models;

namespace DAL.EF
{
  public class VlaggenDbContext : DbContext
  {
    public DbSet<Vlag> Vlags { get; set; }
  }
}