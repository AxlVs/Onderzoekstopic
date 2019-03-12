using Domain;

namespace DAL.EF
{
  public class VlaggenInitializer
  {
    private static bool hasRunDuringAppExecution = false;

    public static void Initialize(VlaggenDbContext context, bool dropCreateDatabase = false)
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
        LandCode = "nl-BE",
        Prijs = 19.99,
        Afbeelding = "../images/vlag/belgie.png"
      };
      
      Vlag v2 = new Vlag()
      {
        ID = 2,
        Beschrijving = "Vlag van Nederland",
        Naam = "Nederland",
        LandCode = "nl-NL",
        Prijs = 19.99,
        Afbeelding = "../images/vlag/nederland.png"
      };
      
      Vlag v3 = new Vlag()
      {
        ID = 3,
        Beschrijving = "Vlag van Frankrijk",
        Naam = "Frankrijk",
        LandCode = "fr-FR",
        Prijs = 24.99,
        Afbeelding = "../images/vlag/frankrijk.png"
      };
      
      Vlag v4 = new Vlag()
      {
        ID = 4,
        Beschrijving = "Vlag van Verenigde Staten",
        Naam = "Verenigde Staten",
        LandCode = "en-US",
        Prijs = 24.99,
        Afbeelding = "../images/vlag/verenigde_staten.png"
      };

      Vlag v5 = new Vlag()
      {
        ID = 5,
        Beschrijving = "Vlag van Duitsland",
        Naam = "Duitsland",
        LandCode = "de-DE",
        Prijs = 24.99,
        Afbeelding = "../images/vlag/duitsland.png"
      };
      
      context.Vlags.Add(v1);
      context.Vlags.Add(v2);
      context.Vlags.Add(v3);
      context.Vlags.Add(v4);
      context.Vlags.Add(v5);

      context.SaveChanges();
    }
  }
}