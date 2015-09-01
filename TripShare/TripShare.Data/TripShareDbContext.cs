namespace TripShare.Data
{
    using System.Data.Entity;

    using Microsoft.AspNet.Identity.EntityFramework;

    using TripShare.Models;
    using TripShare.Data.Migrations;

    public class TripShareDbContext : IdentityDbContext<User>
    {
        public TripShareDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TripShareDbContext, Configuration>());
        }

        public static TripShareDbContext Create()
        {
            return new TripShareDbContext();
        }
    }
}
