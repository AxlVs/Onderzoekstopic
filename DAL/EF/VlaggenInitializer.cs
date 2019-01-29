using ODT.Models;

namespace DAL.EF
{
  public class VlaggenInitializer
  {
    private static bool hasRunDuringAppExecution = false;

    public static void Initialize(VlaggenDbContext context, bool dropCreateDatabase)
    {
      if (!hasRunDuringAppExecution)
      {
        // Delete database if requested
        if (dropCreateDatabase) context.Database.EnsureDeleted();
        // Create database and initial data if needed
        if (context.Database.EnsureCreated()) Seed(context);
        hasRunDuringAppExecution = true;
      }
    }

    private static void Seed(VlaggenDbContext context)
    {
      Vlag v1 = new Vlag()
      {
        ID = 1,
        Beschrijving = "Vlag van België",
        Naam = "Belgie",
        LandCode = "BE",
        Prijs = 19.99
      };
      
      Vlag v2 = new Vlag()
      {
        ID = 2,
        Beschrijving = "Vlag van Nederland",
        Naam = "Nederland",
        LandCode = "NL",
        Prijs = 19.99
      };
      
      Vlag v3 = new Vlag()
      {
        ID = 3,
        Beschrijving = "Vlag van Frankrijk",
        Naam = "Frankrijk",
        LandCode = "FR",
        Prijs = 24.99
      };

      context.Vlags.Add(v1);
      context.Vlags.Add(v3);
      context.Vlags.Add(v2);

      context.SaveChanges();
    }
  }
}