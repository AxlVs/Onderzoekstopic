using System;
using Microsoft.EntityFrameworkCore;
using ODT.Models;

namespace ODT.DAL
{
  public class Vlaggen : DbContext
  {
    public DbSet<Vlag> Vlaggen { get; set; }
  }
}