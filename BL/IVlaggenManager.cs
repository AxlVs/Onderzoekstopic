using System.Collections.Generic;
using ODT.Models;

namespace BL
{
  public interface IVlaggenManager
  {
    IEnumerable<Vlag> GetVlaggen();
    Vlag GetVlag(int vlagId);
  }
}