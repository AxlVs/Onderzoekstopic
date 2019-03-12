using System.Collections.Generic;
using Domain;

namespace BL
{
  public interface IVlaggenManager
  {
    IEnumerable<Vlag> GetVlaggen();
    Vlag GetVlag(int vlagId);
    void AddVlag(Vlag vlag);
  }
}