using System.Collections.Generic;
using ODT.Models;

namespace DAL
{
  public interface IVlaggenRepository
  {
    IEnumerable<Vlag> ReadVlaggen();
    Vlag ReadVlag(int vlagId);
  }
}