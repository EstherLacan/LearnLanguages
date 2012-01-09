namespace LearnLanguages.DataAccess.EFCodeFirst.Migrations
{
  using System;
  using System.Data.Entity;
  using System.Data.Entity.Migrations;
  using System.Linq;

  internal sealed class Configuration : DbMigrationsConfiguration<PhraseContext>
  {
    public Configuration()
    {
      AutomaticMigrationsEnabled = true;
      
      // Seed data: 
      //   Override the Seed method in this class to add seed data.
      //    - The Seed method will be called after migrating to the latest version.
      //    - You can use the DbContext.AddOrUpdate() helper extension method to avoid creating
      //      duplicate seed data. E.g.
      //
      //          myContext.AddOrUpdate(c => c.FullName,
      //              new Customer { FullName = "Andrew Peters", CustomerNumber = 123 },
      //              new Customer { FullName = "Brice Lambson", CustomerNumber = 456 },
      //              new Customer { FullName = "Rowan Miller", CustomerNumber = 789 }
      //          );
      //
    }

    protected override void Seed(PhraseContext context)
    {

      base.Seed(context);
    }
  }
}