using System.Data.Entity.ModelConfiguration.Conventions;

namespace TripShare.Data
{
    using System.Data.Entity;

    using Microsoft.AspNet.Identity.EntityFramework;

    using TripShare.Models;
    using TripShare.Data.Migrations;

    public class TripShareDbContext : IdentityDbContext<User>
    {
        
        public TripShareDbContext()
            : base("TripShare", throwIfV1Schema: false)
        {
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<TripShareDbContext, Configuration>());
        }

     

        public virtual IDbSet<Trip> Trips { get; set; }

        public virtual IDbSet<City> Cities { get; set; }

        public virtual IDbSet<Comment>  Comments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Entity<User>()
                .HasMany(u => u.OwnTrip)
                .WithRequired(p => p.TripOwner)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }

        public static TripShareDbContext Create()
        {
            return new TripShareDbContext();
        }
    }
}
