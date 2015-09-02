namespace TripShare.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TripShare.Models;

    public sealed class Configuration : DbMigrationsConfiguration<TripShareDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
            
        }

        protected override void Seed(TripShareDbContext context)
        {
            if (!context.Cities.Any())
            {
                context.Cities.Add(new City()
                {
                    Name = "Turnovo"
                });

                context.Cities.Add(new City()
                {
                    Name = "Mihaylovgrad"
                });

                context.Cities.Add(new City()
                {
                    Name = "Ruse"
                });

                context.Cities.Add(new City()
                {
                    Name = "Varna"
                });

                context.Cities.Add(new City()
                {
                    Name = "Plovdiv"
                });

                context.Cities.Add(new City()
                {
                    Name = "Sofiq"
                });


                context.Cities.Add(new City()
                {
                    Name = "Burgas"
                });

                context.SaveChanges();
            }
        }
    }
}
