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
                    CityName = "Turnovo"
                });

                context.Cities.Add(new City()
                {
                    CityName = "Mihaylovgrad"
                });

                context.Cities.Add(new City()
                {
                    CityName = "Ruse"
                });

                context.Cities.Add(new City()
                {
                    CityName = "Varna"
                });

                context.Cities.Add(new City()
                {
                    CityName = "Plovdiv"
                });

                context.Cities.Add(new City()
                {
                    CityName = "Sofiq"
                });


                context.Cities.Add(new City()
                {
                    CityName = "Burgas"
                });

                context.SaveChanges();
            }
        }
    }
}
