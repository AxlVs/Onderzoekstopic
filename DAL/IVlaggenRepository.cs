using System.Collections.Generic;
using ODT.Models;

namespace DAL
{
  public interface IVlaggenRepository
  {
    Vlag CreateVlag(Vlag ticket);
    IEnumerable<Vlag> ReadVlaggen();
    Vlag ReadVlag(int vlagId);
    void UpdateVlag(Vlag vlag);
    void DeleteVlag(int vlagId);
  }
}