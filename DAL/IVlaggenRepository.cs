using System.Collections.Generic;
using Domain;

namespace DAL
{
  public interface IVlaggenRepository
  {
    IEnumerable<Vlag> ReadVlaggen();
    Vlag ReadVlag(int vlagId);
    void AddVlag(Vlag vlag);
  }
}