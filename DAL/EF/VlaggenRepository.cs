using System.Collections.Generic;
using System.Linq;
using Domain;

namespace DAL.EF
{
  public class VlaggenRepository : IVlaggenRepository
  {

    private VlaggenDbContext context;

    public VlaggenRepository()
    {
      this.context = new VlaggenDbContext();
    }

    public IEnumerable<Vlag> ReadVlaggen()
    {
      return context.Vlags.AsEnumerable();
    }

    public Vlag ReadVlag(int vlagId)
    {
      return context.Vlags.Single(v => v.ID == vlagId);
    }

    public void AddVlag(Vlag vlag)
    {
      context.Vlags.Add(vlag);
      context.SaveChanges();
    }
  }
}