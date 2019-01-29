using System.Collections.Generic;
using DAL.EF;
using ODT.Models;

namespace BL
{
  public class VlaggenManager : IVlaggenManager
  {
    private VlaggenRepository repo;

    public VlaggenManager(VlaggenRepository repo)
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