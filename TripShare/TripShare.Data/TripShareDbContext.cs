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

        public virtual IDbSet<Notification> Notifications { get; set; }

        public virtual IDbSet<PrivateMessage> PrivateMessages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Entity<Trip>()
                .HasRequired(t => t.Driver)
                .WithMany(u => u.OwnTrips)
                .HasForeignKey(t => t.DriverId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(x => x.JoinedTrips)
                .WithMany(t => t.Passengers);

            base.OnModelCreating(modelBuilder);
        }

        public static TripShareDbContext Create()
        {
            return new TripShareDbContext();
        }
    }
}
