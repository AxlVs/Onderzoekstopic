using Microsoft.EntityFrameworkCore;
using ODT.Models;

namespace DAL.EF
{
  public class VlaggenDbContext
  {
    public DbSet<Vlag> Vlags { get; set; }
  }
}