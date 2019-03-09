using System.Collections.Generic;
using DAL.EF;
using Domain;

namespace BL
{
  public class VlaggenManager : IVlaggenManager
  {
    private VlaggenRepository repo;

    public VlaggenManager()
    {
      this.repo = new VlaggenRepository();
    }

    public IEnumerable<Vlag> GetVlaggen()
    {
      return repo.ReadVlaggen();
    }

    public Vlag GetVlag(int vlagId)
    {
      return repo.ReadVlag(vlagId);
    }
  }
}