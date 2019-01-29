using System.Collections.Generic;
using ODT.Models;

namespace DAL.EF
{
  public class VlaggenRepository : IVlaggenRepository
  {

    private VlaggenDbContext context;

    public VlaggenRepository()
    {
      this.context = new VlaggenDbContext();
      
    }

    public Vlag CreateVlag(Vlag ticket)
    {
      throw new System.NotImplementedException();
    }

    public IEnumerable<Vlag> ReadVlaggen()
    {
      throw new System.NotImplementedException();
    }

    public Vlag ReadVlag(int vlagId)
    {
      throw new System.NotImplementedException();
    }

    public void UpdateVlag(Vlag vlag)
    {
      throw new System.NotImplementedException();
    }

    public void DeleteVlag(int vlagId)
    {
      throw new System.NotImplementedException();
    }
  }
}